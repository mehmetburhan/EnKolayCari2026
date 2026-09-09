using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using EnKolayCari2026.DataBridge.Catalog;
using EnKolayCari2026.DataBridge.Configuration;
using EnKolayCari2026.DataBridge.Sync;
using Microsoft.Extensions.Configuration;

namespace EnKolayCari2026.DataBridge;

public partial class MainWindow : Window
{
    private TransferCatalogRoot? _catalog;
    private CancellationTokenSource? _cts;
    private readonly UserUiState _uiState = UserUiState.Load();
    private bool _suppressCompanyPersist;
    private readonly StringBuilder _logBuilder = new();
    // Tüm satırları tut (filtre için)
    private readonly List<(string Line, LogTone Tone)> _logEntries = new();

    public MainWindow()
    {
        InitializeComponent();
    }

    private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
    {
        try
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: true)
                .Build();

            var connections = new AppConnections();
            config.GetSection("Connections").Bind(connections);
            ApplyConnectionsToUi(connections);

            _catalog = TransferCatalogLoader.Load();
            AppendLog($"Katalog yüklendi: {_catalog.Tables.Count} tablo.");
            AppendLog("Bağlantıları doğrulayıp Bağlan’a basın.");
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message, "Başlatma", MessageBoxButton.OK, MessageBoxImage.Error);
            AppendLog("HATA: " + ex.Message);
        }
    }

    private AppConnections ReadConnectionsFromUi() => new()
    {
        Master = new SqlEndpoint
        {
            Server = TxtMasterServer.Text.Trim(),
            Database = TxtMasterDb.Text.Trim(),
            User = TxtMasterUser.Text.Trim(),
            Password = TxtMasterPassword.Password
        },
        Slave = new SqlEndpoint
        {
            Server = TxtSlaveServer.Text.Trim(),
            Database = TxtSlaveDb.Text.Trim(),
            User = TxtSlaveUser.Text.Trim(),
            Password = TxtSlavePassword.Password
        },
        Target = new SqlEndpoint
        {
            Server = TxtTargetServer.Text.Trim(),
            Database = TxtTargetDb.Text.Trim(),
            User = TxtTargetUser.Text.Trim(),
            Password = TxtTargetPassword.Password
        }
    };

    private void ApplyConnectionsToUi(AppConnections c)
    {
        TxtMasterServer.Text = c.Master.Server;
        TxtMasterDb.Text = c.Master.Database;
        TxtMasterUser.Text = c.Master.User;
        TxtMasterPassword.Password = c.Master.Password;

        TxtSlaveServer.Text = c.Slave.Server;
        TxtSlaveDb.Text = c.Slave.Database;
        TxtSlaveUser.Text = c.Slave.User;
        TxtSlavePassword.Password = c.Slave.Password;

        TxtTargetServer.Text = c.Target.Server;
        TxtTargetDb.Text = c.Target.Database;
        TxtTargetUser.Text = c.Target.User;
        TxtTargetPassword.Password = c.Target.Password;
    }

    private TransferOptions ReadOptions()
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: true)
            .Build();
        var opt = new TransferOptions();
        config.GetSection("Transfer").Bind(opt);
        if (opt.BatchSize <= 0) opt.BatchSize = 100;
        return opt;
    }

    private TransferOrchestrator CreateOrchestrator(IProgress<TransferProgress>? progress = null) =>
        new(ReadConnectionsFromUi(), ReadOptions(), _catalog ?? TransferCatalogLoader.Load(), progress);

    private async void BtnConnect_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            SetBusy(true);
            var progress = new Progress<TransferProgress>(p => AppendLog(p.Message));
            var orch = CreateOrchestrator(progress);
            await orch.TestConnectionsAsync();
            await LoadCompaniesIntoComboAsync(orch);
            WorkspacePanel.Visibility = Visibility.Visible;
            ConnectionsBox.Visibility = Visibility.Collapsed;
            BtnToggleConnections.Content = "Bağlantıları göster";
            AppendLog("Bağlandı — çalışma alanı açıldı.");
        }
        catch (Exception ex)
        {
            WorkspacePanel.Visibility = Visibility.Collapsed;
            MessageBox.Show(this, ex.Message, "Bağlan", MessageBoxButton.OK, MessageBoxImage.Error);
            AppendLog("HATA: " + ex.Message);
        }
        finally
        {
            SetBusy(false);
        }
    }

    private async void BtnTest_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            SetBusy(true);
            var progress = new Progress<TransferProgress>(p => AppendLog(p.Message));
            await CreateOrchestrator(progress).TestConnectionsAsync();
            MessageBox.Show(this, "Tüm bağlantılar başarılı.", "Test", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message, "Bağlantı hatası", MessageBoxButton.OK, MessageBoxImage.Error);
            AppendLog("HATA: " + ex.Message);
        }
        finally
        {
            SetBusy(false);
        }
    }

    private async void BtnLoadCompanies_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            SetBusy(true);
            await LoadCompaniesIntoComboAsync(CreateOrchestrator());
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message, "Şirket listesi", MessageBoxButton.OK, MessageBoxImage.Error);
            AppendLog("HATA: " + ex.Message);
        }
        finally
        {
            SetBusy(false);
        }
    }

    private async Task LoadCompaniesIntoComboAsync(TransferOrchestrator orch)
    {
        var list = await orch.LoadCompaniesAsync();
        _suppressCompanyPersist = true;
        try
        {
            CmbCompanies.ItemsSource = list;
            if (list.Count == 0)
            {
                CmbCompanies.SelectedIndex = -1;
            }
            else
            {
                var lastId = _uiState.LastLegacyCompanyId;
                var match = lastId > 0
                    ? list.FirstOrDefault(c => c.LegacyId == lastId)
                    : null;
                CmbCompanies.SelectedItem = match ?? list[0];
            }
        }
        finally
        {
            _suppressCompanyPersist = false;
        }

        AppendLog($"{list.Count} şirket yüklendi.");
        if (CmbCompanies.SelectedItem is CompanyListItem sel)
            AppendLog($"Seçili şirket: {sel.Display}");
    }

    private void CmbCompanies_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (_suppressCompanyPersist)
            return;
        if (CmbCompanies.SelectedItem is not CompanyListItem company)
            return;
        _uiState.LastLegacyCompanyId = company.LegacyId;
        _uiState.Save();
    }

    private async void BtnPreview_OnClick(object sender, RoutedEventArgs e)
    {
        if (CmbCompanies.SelectedItem is not CompanyListItem company)
        {
            MessageBox.Show(this, "Önce şirket seçin.", "Önizleme", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        try
        {
            SetBusy(true);
            var progress = new Progress<TransferProgress>(p =>
            {
                TxtProgress.Text = p.Message;
                ProgressBar.Maximum = Math.Max(p.Total, 1);
                ProgressBar.Value = p.Current;
            });
            var counts = await CreateOrchestrator(progress).PreviewCountsAsync(company.LegacyId);
            var sb = new StringBuilder();
            sb.AppendLine($"Şirket: {company.Display}");
            sb.AppendLine($"Toplam satır (filtreli): {counts.Where(kv => kv.Value > 0).Sum(kv => kv.Value):N0}");
            sb.AppendLine();
            foreach (var kv in counts.OrderBy(x => x.Key))
            {
                var mark = kv.Value < 0 ? "ERR" : kv.Value.ToString("N0");
                sb.AppendLine($"{kv.Key,-28} {mark}");
            }
            SetPreviewDocument(sb.ToString());
            AppendLog("Önizleme tamam.");
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message, "Önizleme", MessageBoxButton.OK, MessageBoxImage.Error);
            AppendLog("HATA: " + ex.Message);
        }
        finally
        {
            SetBusy(false);
        }
    }

    private async void BtnTransfer_OnClick(object sender, RoutedEventArgs e)
    {
        if (CmbCompanies.SelectedItem is not CompanyListItem company)
        {
            MessageBox.Show(this, "Önce şirket seçin.", "Aktarım", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        var confirm = MessageBox.Show(this,
            $"'{company.Display}' için tüm katalog tabloları EKCN2026'ya aktarılacak.\nDevam?",
            "Aktarım onayı",
            MessageBoxButton.YesNo,
            MessageBoxImage.Question);
        if (confirm != MessageBoxResult.Yes)
            return;

        _uiState.LastLegacyCompanyId = company.LegacyId;
        _uiState.Save();

        _cts = new CancellationTokenSource();
        try
        {
            SetBusy(true, transferring: true);
            ResetLiveStats();
            var progress = new Progress<TransferProgress>(p =>
            {
                UpdateLiveStats(p);
                TxtProgress.Text = p.Message;
                ProgressBar.Maximum = Math.Max(p.Total, 1);
                ProgressBar.Value = p.Current;
                if (ShouldLogProgress(p.Message))
                    AppendLog(p.Message);
            });

            var result = await CreateOrchestrator(progress).RunAsync(company, _cts.Token);
            var sb = new StringBuilder();
            sb.AppendLine($"Başlangıç: {result.StartedAt:G}");
            sb.AppendLine($"Bitiş: {result.FinishedAt:G}");
            sb.AppendLine($"Legacy CompanyId: {result.LegacyCompanyId} → New: {result.NewCompanyId}");
            sb.AppendLine($"Inserted={result.TotalInserted:N0}  Skipped={result.TotalSkipped:N0}  Failed={result.TotalFailed:N0}");
            if (result.Cancelled) sb.AppendLine("IPTAL EDİLDİ");
            sb.AppendLine();
            foreach (var t in result.Tables)
            {
                sb.AppendLine($"{t.LegacyTable,-28} src={t.SourceCount,6} ins={t.Inserted,6} skip={t.Skipped,6} fail={t.Failed,4}");
                foreach (var err in t.Errors.Take(10))
                    sb.AppendLine($"    ! {err}");
                if (t.Errors.Count > 10)
                    sb.AppendLine($"    … +{t.Errors.Count - 10} fail notu daha (Log’u panoya kopyala)");
            }
            foreach (var err in result.Errors)
                sb.AppendLine("RUN: " + err);

            SetPreviewDocument(sb.ToString());
            UpdateLiveStatsTotals(result.TotalInserted, result.TotalSkipped, result.TotalFailed);
            TxtProgress.Text = $"Aktarım bitti — Success={result.TotalInserted:N0} Skip={result.TotalSkipped:N0} Fail={result.TotalFailed:N0}";
            AppendLog("Aktarım bitti.");
            if (result.TotalFailed > 0)
                AppendLog($"Özet: {result.TotalFailed:N0} fail — detaylar için Önizleme / sonuç paneli.");
            TxtPreview.Focus();
            MessageBox.Show(this,
                $"Aktarım tamam.\nSuccess: {result.TotalInserted:N0}\nSkip: {result.TotalSkipped:N0}\nFail: {result.TotalFailed:N0}",
                "Sonuç",
                MessageBoxButton.OK,
                result.TotalFailed > 0 ? MessageBoxImage.Warning : MessageBoxImage.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message, "Aktarım", MessageBoxButton.OK, MessageBoxImage.Error);
            AppendLog("HATA: " + ex.Message);
        }
        finally
        {
            _cts?.Dispose();
            _cts = null;
            SetBusy(false);
        }
    }

    private void BtnCancel_OnClick(object sender, RoutedEventArgs e)
    {
        _cts?.Cancel();
        AppendLog("İptal isteniyor…");
    }

    private async void BtnReset_OnClick(object sender, RoutedEventArgs e)
    {
        if (CmbCompanies.SelectedItem is not CompanyListItem company)
        {
            MessageBox.Show(this, "Önce şirket seçin.", "Sil Baştan", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        // 1. Uyarı: silme onayı
        var confirm1 = MessageBox.Show(this,
            $"⚠ DİKKAT!\n\n'{company.Display}' şirketine ait\nHEDEF veritabanındaki TÜM veriler SİLİNECEK.\n\nBu işlem geri alınamaz!\n\nDevam etmek istiyor musunuz?",
            "Sil Baştan — 1. Onay",
            MessageBoxButton.YesNo,
            MessageBoxImage.Warning);
        if (confirm1 != MessageBoxResult.Yes) return;

        // 2. Onay: transfer da başlatılsın mı?
        var confirm2 = MessageBox.Show(this,
            "Veriler silindikten hemen sonra aktarım da başlatılsın mı?\n\n" +
            "  Evet  →  Sil + Aktarım\n" +
            "  Hayır →  Sadece Sil",
            "Sil Baştan — Aktarım?",
            MessageBoxButton.YesNoCancel,
            MessageBoxImage.Question);
        if (confirm2 == MessageBoxResult.Cancel) return;

        var runTransferAfter = confirm2 == MessageBoxResult.Yes;

        _cts = new CancellationTokenSource();
        try
        {
            SetBusy(true, transferring: true);
            AppendLog($"Temizleme başlıyor: {company.Display}");

            var progress = new Progress<TransferProgress>(p =>
            {
                TxtProgress.Text = p.Message;
                if (ShouldLogProgress(p.Message))
                    AppendLog(p.Message);
            });

            var cleanResult = await CreateOrchestrator(progress).CleanCompanyAsync(company, _cts.Token);

            // Temizleme özetini göster
            var sb = new StringBuilder();
            sb.AppendLine($"=== Temizleme Sonucu ===");
            sb.AppendLine($"Şirket: {company.Display}");
            sb.AppendLine($"Toplam silinen satır: {cleanResult.TotalDeleted:N0}");
            sb.AppendLine();
            foreach (var kv in cleanResult.DeletedRows.Where(kv => kv.Value > 0))
                sb.AppendLine($"  {kv.Key,-40} {kv.Value,6} satır silindi");
            if (cleanResult.Warnings.Count > 0)
            {
                sb.AppendLine();
                sb.AppendLine("Uyarılar:");
                foreach (var w in cleanResult.Warnings) sb.AppendLine($"  ! {w}");
            }
            if (cleanResult.HasErrors)
            {
                sb.AppendLine();
                sb.AppendLine("HATALAR:");
                foreach (var err in cleanResult.Errors) sb.AppendLine($"  !! {err}");
            }
            SetPreviewDocument(sb.ToString());
            AppendLog($"Temizleme bitti — {cleanResult.TotalDeleted:N0} satır silindi.");

            if (cleanResult.HasErrors)
            {
                MessageBox.Show(this,
                    $"Temizleme tamamlandı ancak bazı hatalar oluştu.\nDetaylar için Önizleme paneline bakın.",
                    "Sil Baştan", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        catch (OperationCanceledException)
        {
            AppendLog("Temizleme iptal edildi.");
            return;
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message, "Sil Baştan — Hata", MessageBoxButton.OK, MessageBoxImage.Error);
            AppendLog("HATA: " + ex.Message);
            return;
        }
        finally
        {
            if (!runTransferAfter)
            {
                _cts?.Dispose();
                _cts = null;
                SetBusy(false);
            }
        }

        if (!runTransferAfter) return;

        // Temizleme sonrası aktarımı başlat (mevcut _cts'yi yenile)
        _cts?.Dispose();
        _cts = new CancellationTokenSource();
        try
        {
            ResetLiveStats();
            AppendLog($"Aktarım başlıyor: {company.Display}");
            var progress2 = new Progress<TransferProgress>(p =>
            {
                UpdateLiveStats(p);
                TxtProgress.Text = p.Message;
                ProgressBar.Maximum = Math.Max(p.Total, 1);
                ProgressBar.Value = p.Current;
                if (ShouldLogProgress(p.Message))
                    AppendLog(p.Message);
            });

            var result = await CreateOrchestrator(progress2).RunAsync(company, _cts.Token);
            var sb2 = new StringBuilder();
            sb2.AppendLine($"=== Aktarım Sonucu ===");
            sb2.AppendLine($"Başlangıç: {result.StartedAt:G}   Bitiş: {result.FinishedAt:G}");
            sb2.AppendLine($"Legacy CompanyId: {result.LegacyCompanyId} → New: {result.NewCompanyId}");
            sb2.AppendLine($"Inserted={result.TotalInserted:N0}  Skipped={result.TotalSkipped:N0}  Failed={result.TotalFailed:N0}");
            if (result.Cancelled) sb2.AppendLine("IPTAL EDİLDİ");
            sb2.AppendLine();
            foreach (var t in result.Tables)
            {
                sb2.AppendLine($"{t.LegacyTable,-28} src={t.SourceCount,6} ins={t.Inserted,6} skip={t.Skipped,6} fail={t.Failed,4}");
                foreach (var err in t.Errors.Take(10))
                    sb2.AppendLine($"    ! {err}");
                if (t.Errors.Count > 10)
                    sb2.AppendLine($"    … +{t.Errors.Count - 10} fail notu daha");
            }
            foreach (var err in result.Errors)
                sb2.AppendLine("RUN: " + err);

            SetPreviewDocument(sb2.ToString());
            UpdateLiveStatsTotals(result.TotalInserted, result.TotalSkipped, result.TotalFailed);
            TxtProgress.Text = $"Aktarım bitti — Success={result.TotalInserted:N0} Skip={result.TotalSkipped:N0} Fail={result.TotalFailed:N0}";
            AppendLog("Aktarım bitti.");
            TxtPreview.Focus();
            MessageBox.Show(this,
                $"Sil Baştan + Aktarım tamam.\nSuccess: {result.TotalInserted:N0}\nSkip: {result.TotalSkipped:N0}\nFail: {result.TotalFailed:N0}",
                "Sonuç",
                MessageBoxButton.OK,
                result.TotalFailed > 0 ? MessageBoxImage.Warning : MessageBoxImage.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message, "Aktarım", MessageBoxButton.OK, MessageBoxImage.Error);
            AppendLog("HATA: " + ex.Message);
        }
        finally
        {
            _cts?.Dispose();
            _cts = null;
            SetBusy(false);
        }
    }

    private void BtnToggleConnections_OnClick(object sender, RoutedEventArgs e)
    {
        var hide = ConnectionsBox.Visibility == Visibility.Visible;
        ConnectionsBox.Visibility = hide ? Visibility.Collapsed : Visibility.Visible;
        BtnToggleConnections.Content = hide ? "Bağlantıları göster" : "Bağlantıları gizle";
    }

    private void BtnCopyLog_OnClick(object sender, RoutedEventArgs e)
    {
        CopyToClipboard(GetFullLogText(), "Log");
    }

    private void BtnCopyLogSelection_OnClick(object sender, RoutedEventArgs e)
    {
        CopySelectionFrom(TxtLog, "Log seçimi");
    }

    private void BtnCopyPreview_OnClick(object sender, RoutedEventArgs e)
    {
        CopyToClipboard(GetRichTextPlain(TxtPreview), "Sonuç");
    }

    private void BtnCopyPreviewSelection_OnClick(object sender, RoutedEventArgs e)
    {
        CopySelectionFrom(TxtPreview, "Sonuç seçimi");
    }

    private void BtnClearLog_OnClick(object sender, RoutedEventArgs e)
    {
        _logBuilder.Clear();
        _logEntries.Clear();
        TxtLog.Document.Blocks.Clear();
    }

    private void ChkFailOnly_Changed(object sender, RoutedEventArgs e)
    {
        RebuildLogView();
    }

    private void CopySelectionFrom(RichTextBox box, string label)
    {
        var selected = box.Selection?.Text?.TrimEnd('\r', '\n') ?? "";
        if (string.IsNullOrWhiteSpace(selected))
        {
            MessageBox.Show(this, "Önce metni fareyle işaretleyin.\n(Ctrl+C de kullanılabilir.)",
                label, MessageBoxButton.OK, MessageBoxImage.Information);
            return;
        }
        CopyToClipboard(selected, label, echoToLog: false);
    }

    private void CopyToClipboard(string text, string label, bool echoToLog = true)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            MessageBox.Show(this, $"{label} boş.", "Pano", MessageBoxButton.OK, MessageBoxImage.Information);
            return;
        }

        try
        {
            Clipboard.SetText(text);
            if (echoToLog)
                AppendLog($"{label} panoya kopyalandı ({text.Length:N0} karakter).");
            else
                TxtProgress.Text = $"{label} panoya kopyalandı ({text.Length:N0} karakter).";
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, "Panoya kopyalanamadı: " + ex.Message, "Pano", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }

    private string GetFullLogText() =>
        _logBuilder.Length > 0 ? _logBuilder.ToString() : GetRichTextPlain(TxtLog);

    private static string GetRichTextPlain(RichTextBox box) =>
        new TextRange(box.Document.ContentStart, box.Document.ContentEnd).Text.TrimEnd();

    private void SetBusy(bool busy, bool transferring = false)
    {
        BtnConnect.IsEnabled = !busy;
        BtnPreview.IsEnabled = !busy;
        BtnTransfer.IsEnabled = !busy;
        BtnReset.IsEnabled = !busy;
        BtnCancel.IsEnabled = transferring;
        CmbCompanies.IsEnabled = !busy;
    }

    private void ResetLiveStats() => UpdateLiveStatsTotals(0, 0, 0);

    private void UpdateLiveStats(TransferProgress p) =>
        UpdateLiveStatsTotals(p.Inserted, p.Skipped, p.Failed);

    private void UpdateLiveStatsTotals(int success, int skip, int fail)
    {
        TxtStatSuccess.Text = success.ToString("N0");
        TxtStatSkip.Text = skip.ToString("N0");
        TxtStatFail.Text = fail.ToString("N0");
    }

    private static bool ShouldLogProgress(string message)
    {
        if (string.IsNullOrEmpty(message))
            return false;

        // Her fail/hata mesajı mutlaka log'a düşsün (neden fail görülsün)
        if (message.Contains("FAIL", StringComparison.OrdinalIgnoreCase) ||
            message.Contains("HATA", StringComparison.OrdinalIgnoreCase) ||
            message.Contains("ÇÖZÜM", StringComparison.OrdinalIgnoreCase))
            return true;

        // Tablo başlangıcı ve bitiş özeti (sadece fail > 0 ise, yoksa gürültü olur)
        if (message.Contains("başlıyor", StringComparison.OrdinalIgnoreCase))
            return true;
        if (message.Contains("bitti", StringComparison.OrdinalIgnoreCase))
        {
            // "bitti: ins=X, skip=Y, fail=0" → fail=0 ise log'a alma (gürültü azalt)
            var fm = System.Text.RegularExpressions.Regex.Match(message, @"fail\s*=\s*(\d+)", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            if (fm.Success && int.TryParse(fm.Groups[1].Value, out var failN) && failN == 0)
                return false;
            return true;
        }

        // Aktarım genel mesajları ve CompanyId bildirimi
        if (message.Contains("Aktarım", StringComparison.OrdinalIgnoreCase) ||
            message.Contains("CompanyId", StringComparison.OrdinalIgnoreCase) ||
            message.Contains("Temizleme", StringComparison.OrdinalIgnoreCase))
            return true;

        return false;
    }

    private static readonly Brush LogOkBrush = CreateFrozenBrush(0x1B, 0x7A, 0x3A);
    private static readonly Brush LogSkipBrush = CreateFrozenBrush(0xB8, 0x86, 0x0B);
    private static readonly Brush LogFailBrush = CreateFrozenBrush(0xC6, 0x28, 0x28);
    private static readonly Regex FailCountRegex = new(@"fail\s*=\s*(\d+)", RegexOptions.IgnoreCase | RegexOptions.Compiled);
    private static readonly Regex SkipCountRegex = new(@"skip(?:ped)?\s*=\s*(\d+)", RegexOptions.IgnoreCase | RegexOptions.Compiled);

    private static Brush CreateFrozenBrush(byte r, byte g, byte b)
    {
        var brush = new SolidColorBrush(Color.FromRgb(r, g, b));
        brush.Freeze();
        return brush;
    }

    private void AppendLog(string message)
    {
        var line = $"[{DateTime.Now:HH:mm:ss}] {message}";
        if (_logBuilder.Length > 0)
            _logBuilder.AppendLine();
        _logBuilder.Append(line);

        var tone = GetLogTone(message);
        _logEntries.Add((line, tone));

        if (TxtLog == null)
            return;

        // Filtre aktifse sadece Fail satırları ekle
        var failOnly = ChkFailOnly?.IsChecked == true;
        if (!failOnly || tone == LogTone.Fail)
        {
            PrependColoredLine(TxtLog, line, BrushForTone(tone));

            const int maxBlocks = 2_000;
            var blocks = TxtLog.Document.Blocks;
            while (blocks.Count > maxBlocks && blocks.LastBlock != null)
                blocks.Remove(blocks.LastBlock);
        }
    }

    /// <summary>
    /// Log görünümünü mevcut filtre durumuna göre tamamen yeniden oluşturur.
    /// </summary>
    private void RebuildLogView()
    {
        var doc = TxtLog.Document;
        doc.Blocks.Clear();

        var failOnly = ChkFailOnly?.IsChecked == true;
        // Yeni satırlar üste eklendiğinden, _logEntries tersine dolaşılıyor
        for (var i = _logEntries.Count - 1; i >= 0; i--)
        {
            var (line, tone) = _logEntries[i];
            if (failOnly && tone != LogTone.Fail)
                continue;

            var para = new Paragraph(new Run(line) { Foreground = BrushForTone(tone) })
            {
                Margin = new Thickness(0, 0, 0, 2)
            };
            doc.Blocks.Add(para);
        }
    }

    private void SetPreviewDocument(string text)
    {
        var doc = new FlowDocument
        {
            PagePadding = new Thickness(4),
            FontFamily = new FontFamily("Consolas"),
            FontSize = 12
        };

        if (!string.IsNullOrEmpty(text))
        {
            foreach (var raw in text.Replace("\r\n", "\n").Split('\n'))
            {
                var line = raw.TrimEnd();
                doc.Blocks.Add(new Paragraph(new Run(line) { Foreground = BrushForMessage(line) })
                {
                    Margin = new Thickness(0, 0, 0, 1)
                });
            }
        }

        TxtPreview.Document = doc;
        TxtPreview.ScrollToHome();
    }

    private static void PrependColoredLine(RichTextBox box, string line, Brush brush)
    {
        var para = new Paragraph(new Run(line) { Foreground = brush })
        {
            Margin = new Thickness(0, 0, 0, 2)
        };
        var blocks = box.Document.Blocks;
        if (blocks.FirstBlock != null)
            blocks.InsertBefore(blocks.FirstBlock, para);
        else
            blocks.Add(para);
    }

    private static Brush BrushForTone(LogTone tone) =>
        tone switch
        {
            LogTone.Fail => LogFailBrush,
            LogTone.Skip => LogSkipBrush,
            _ => LogOkBrush
        };

    private static Brush BrushForMessage(string message) =>
        BrushForTone(GetLogTone(message));

    private enum LogTone { Ok, Skip, Fail }

    private static LogTone GetLogTone(string message)
    {
        if (string.IsNullOrEmpty(message))
            return LogTone.Ok;

        if (message.Contains("FAIL", StringComparison.OrdinalIgnoreCase) ||
            message.Contains("HATA", StringComparison.OrdinalIgnoreCase) ||
            message.Contains("IPTAL", StringComparison.OrdinalIgnoreCase))
            return LogTone.Fail;

        var failM = FailCountRegex.Match(message);
        if (failM.Success && int.TryParse(failM.Groups[1].Value, out var failN) && failN > 0)
            return LogTone.Fail;

        if (message.Contains(" ERR", StringComparison.Ordinal) ||
            message.EndsWith("ERR", StringComparison.Ordinal))
            return LogTone.Fail;

        var skipM = SkipCountRegex.Match(message);
        if (skipM.Success && int.TryParse(skipM.Groups[1].Value, out var skipN) && skipN > 0)
            return LogTone.Skip;

        return LogTone.Ok;
    }
}

/// <summary>Son seçilen şirket vb. kullanıcı tercihleri (LocalAppData).</summary>
internal sealed class UserUiState
{
    public long LastLegacyCompanyId { get; set; }

    private static string FilePath =>
        Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "EnKolayCari2026.DataBridge",
            "ui-state.json");

    public static UserUiState Load()
    {
        try
        {
            var path = FilePath;
            if (!File.Exists(path))
                return new UserUiState();
            var json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<UserUiState>(json) ?? new UserUiState();
        }
        catch
        {
            return new UserUiState();
        }
    }

    public void Save()
    {
        try
        {
            var path = FilePath;
            var dir = Path.GetDirectoryName(path);
            if (!string.IsNullOrEmpty(dir))
                Directory.CreateDirectory(dir);
            File.WriteAllText(path, JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true }));
        }
        catch
        {
            // tercih kaydı kritik değil
        }
    }
}
