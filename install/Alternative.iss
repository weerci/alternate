; Скрипт создан через Мастер Inno Setup Script.
; ИСПОЛЬЗУЙТЕ ДОКУМЕНТАЦИЮ ДЛЯ ПОДРОБНОСТЕЙ ИСПОЛЬЗОВАНИЯ INNO SETUP!

#define MyAppName "Альтернатива-Лояльность"
#define MyAppVersion GetFileVersion('D:\datadi\programming\exe\Alternative\Alternative.exe')
#define MyAppPublisher "Kriate"
#define MyAppURL "https://github.com/weerci/alternative/issues"
#define MyAppExeName "Alternative.exe"
#define ApplicationName "Альтернатива-Лояльность"
#define ApplicationVersion GetFileVersion('Application.exe')

[Setup]
WizardSmallImageFile=D:\datadi\programming\src\alternate\image\001.bmp
WizardImageFile=D:\datadi\programming\src\alternate\image\001.bmp
; Примечание: Значение AppId идентифицирует это приложение.
; Не используйте одно и тоже значение в разных установках.
; (Для генерации значения GUID, нажмите Инструменты | Генерация GUID.)
AppId={{12F5D167-AD4B-4181-8C97-86CDF4805A73}
AppName={#ApplicationName}
AppVersion={#ApplicationVersion}
AppVerName={#ApplicationName} {#ApplicationVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
DefaultDirName={pf}\kriate\alternative
DefaultGroupName=Alternative
OutputDir=D:\datadi\programming\exe\alternative
OutputBaseFilename=Alt_setup_{#MyAppVersion}
SetupIconFile=D:\datadi\programming\src\alternate\image\coins.ico
Compression=lzma
SolidCompression=yes
AppMutex=12F5D167-AD4B-4181-8C97-86CDF4805A73

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; 
Name: "quicklaunchicon"; Description: "{cm:CreateQuickLaunchIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked; 

[Files]
Source: ISAlliance.dll; DestDir: {app};
;{ ISFormDesignerFilesBegin } // Не удалять эту строку!
;// Не изменять эту секцию. Она создана автоматически.
DestName: "WizardForm.WizardBitmapImage2.bmp"; Source: "D:\datadi\programming\src\alternate\image\001.bmp"; Flags: dontcopy solidbreak
;// Не изменять эту секцию. Она создана автоматически.
;{ ISFormDesignerFilesEnd } // Не удалять эту строку!

Source: ISAlliance.dll; DestDir: {app};
Source: "D:\datadi\programming\exe\alternative\Alternative.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\datadi\programming\exe\alternative\Alternative.ver"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\datadi\programming\exe\alternative\Alternative.exe.config"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\datadi\programming\exe\alternative\Alternative.chm"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\datadi\programming\src\alternate\outerdll\CoreCommon.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\datadi\programming\exe\alternative\System.Data.SQLite.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\datadi\programming\exe\alternative\SQLite.Interop.dll"; DestDir: "{app}"; Flags: ignoreversion

[Icons]
Name: "{group}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{group}\{cm:UninstallProgram,{#MyAppName}}"; Filename: "{uninstallexe}"
Name: "{commondesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon
Name: "{userappdata}\Microsoft\Internet Explorer\Quick Launch\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: quicklaunchicon

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent
Filename: "dummy"; Description: "Загружать программу при старте ОС"; Flags: postinstall nowait skipifdoesntexist unchecked

[ISFormDesigner]
WizardForm=FF0A005457495A415244464F524D003010DB03000054504630F10B5457697A617264466F726D0A57697A617264466F726D0C436C69656E744865696768740368010B436C69656E74576964746803F1010C4578706C696369744C65667402000B4578706C69636974546F7002000D4578706C6963697457696474680301020E4578706C69636974486569676874038F010D506978656C73506572496E636802600A54657874486569676874020D00F10C544E65774E6F7465626F6F6B0D4F757465724E6F7465626F6F6B00F110544E65774E6F7465626F6F6B506167650B57656C636F6D65506167650D4578706C69636974576964746803F1010E4578706C696369744865696768740339010000F110544E65774E6F7465626F6F6B5061676509496E6E6572506167650D4578706C69636974576964746803F1010E4578706C6963697448656967687403390100F10C544E65774E6F7465626F6F6B0D496E6E65724E6F7465626F6F6B00F110544E65774E6F7465626F6F6B506167650D53656C656374446972506167650D4578706C69636974576964746803A1010E4578706C6963697448656967687403ED000000F110544E65774E6F7465626F6F6B506167651653656C65637450726F6772616D47726F7570506167650D4578706C69636974576964746803A1010E4578706C6963697448656967687403ED000000F110544E65774E6F7465626F6F6B506167650F53656C6563745461736B73506167650D4578706C69636974576964746803A1010E4578706C6963697448656967687403ED000000F110544E65774E6F7465626F6F6B50616765095265616479506167650D4578706C69636974576964746803A1010E4578706C6963697448656967687403ED000000F110544E65774E6F7465626F6F6B506167650E496E7374616C6C696E67506167650D4578706C69636974576964746803A1010E4578706C6963697448656967687403ED00000000F1065450616E656C094D61696E50616E656C00F10C544269746D6170496D6167651657697A617264536D616C6C4269746D6170496D6167650A4269746D617046696C650632443A5C6461746164695C70726F6772616D6D696E675C7372635C6B72697061796D656E745C696D6167655C3030312E626D7000000000F110544E65774E6F7465626F6F6B506167650C46696E6973686564506167650D4578706C69636974576964746803F1010E4578706C6963697448656967687403390100F10C544269746D6170496D6167651257697A6172644269746D6170496D616765320A4269746D617046696C650632443A5C6461746164695C70726F6772616D6D696E675C7372635C6B72697061796D656E745C696D6167655C3030312E626D700000000000

[UninstallDelete]
Name: "{app}\ISAlliance.dll"; Type: files;

[Code]
{ RedesignWizardFormBegin } // Не удалять эту строку!
// Не изменять эту секцию. Она создана автоматически.
procedure RedesignWizardForm;
begin
  with WizardForm.WizardBitmapImage2 do
  begin
    ExtractTemporaryFile('WizardForm.WizardBitmapImage2.bmp');
    Bitmap.LoadFromFile(ExpandConstant('{tmp}\WizardForm.WizardBitmapImage2.bmp'));
  end;

{ ReservationBegin }
  // Вы можете добавить ваш код здесь.
{ ReservationEnd }
end;
// Не изменять эту секцию. Она создана автоматически.
{ RedesignWizardFormEnd } // Не удалять эту строку!

procedure InitializeWizard();
begin
  RedesignWizardForm;
end;

{ RedesignWizardFormBegin } // Не удалять эту строку!
// Не изменять эту секцию. Она создана автоматически.
procedure KillProc(lpProcName: AnsiString); external 'KillProcess@{app}\ISAlliance.dll stdcall delayload uninstallonly';


procedure CurStepChanged(CurStep: TSetupStep);
var r: Integer;
begin
    if CurStep = ssInstall then
    begin
        Exec(ExpandConstant('{app}\unins000.exe'), '/VERYSILENT /SUPPRESSMSGBOXES', '', SW_SHOW, ewWaitUntilTerminated, r);
        // Дожидаемся завершения удаления...
        while CheckForMutexes('{#MyAppName}Mutex') do Sleep(100);
    end;

    if CurStep = ssDone then
        if WizardForm.RunList.Checked[1] then
            CreateShellLink(ExpandConstant('{userstartup}\{#MyAppName}.lnk'),
                            '',
                            ExpandConstant('{app}\{#MyAppExeName}'),
                            '',
                            ExpandConstant('{app}'),
                            '',
                            0,
                            SW_SHOW);
end;

procedure CurUninstallStepChanged(CurUninstallStep: TUninstallStep);
begin
  if CurUninstallStep = usUninstall then
  begin
    UnloadDLL(ExpandConstant('{app}\ISAlliance.dll'));
  end;
    if CurUninstallStep = usDone then
        DeleteFile(ExpandConstant('{userstartup}\{#MyAppName}.lnk'));
end;

function InitializeUninstall(): Boolean;
begin
  KillProc('{#MyAppExeName}');
  Result := true;
end;

function GetVersion(): string;
begin
  Result := '1.1.5135.40225';
end;



