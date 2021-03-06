// -x- procedural nightmare...refactor!
unit Global;

interface

uses
  Windows, Forms, ShellApi, SysUtils, Classes, SyncObjs, Controls, Graphics, Math,
  PaletteIO, ControlPoint, Xform, CommDlg;

type
  EFormatInvalid = class(Exception);
  TIntegerArray = array [0 .. 9] of Integer;

type
  TWin32Version = (wvUnknown, wvWin95, wvWin98, wvWinNT, wvWin2000, wvWinXP, wvWinVista, wvWin7, wvWinFutureFromOuterSpace);

function triangle_area(t: TTriangle): double;
function transform_affine(const t: TTriangle;
  const Triangles: TTriangles): boolean;
function line_dist(x, y, x1, y1, x2, y2: double): double;
function dist(x1, y1, x2, y2: double): double;
function GetVal(token: string): string;
function ReplaceTabs(str: string): string;
function det(a, b, c, d: double): double;
function solve3(x1, x2, x1h, y1, y2, y1h, z1, z2, z1h: double;
  var a, b, e: double): double;
function OpenSaveFileDialog(Parent: TWinControl;
  const DefExt, Filter, InitialDir, Title: string; var FileName: string;
  MustExist, OverwritePrompt, NoChangeDir, DoOpen: boolean): boolean;
function GetEnvVarValue(const VarName: string): string;
function GradientString(c: TColorMap): string;
function WinShellOpen(const AssociatedFile: string): boolean;
function GetWinVersion: TWin32Version;
function CleanIdentifier(ident: string): string;
function CleanUPRTitle(ident: string): string;
function EntryExists(En, Fl: string): boolean;
function DeleteEntry(Entry, FileName: string): boolean;
procedure swapcolor(var clist: array of cardinal; i, j: integer);
function diffcolor(clist: array of cardinal; i, j: integer): cardinal;
function ScaleRect(r: TRect; scale: double): TSRect;
function WinExecAndWait32(FileName: string): integer;


const
  APP_NAME: string = 'Apophysis/XW';
  APP_VERSION: string = 'Version 3.00.7621';
{$IFDEF Apo7X64}
  APP_BUILD: string = ' - 64 bit';
{$ELSE}
  APP_BUILD: string = ' - 32 bit';
{$ENDIF}
  MAX_TRANSFORMS: Integer = 100;
  prefilter_white: Integer = 1024;
  eps: double = 1E-10;
  White_level = 200;
  clyellow1 = TColor($17FCFF);
  clplum2 = TColor($ECA9E6);
  clSlateGray = TColor($837365);
  FT_BMP = 1;
  FT_PNG = 2;
  FT_JPG = 3;

const
  crEditArrow = 20;
  crEditMove = 21;
  crEditRotate = 22;
  crEditScale = 23;

const
  SingleBuffer: boolean =
{$IFDEF Apo7X64}
    false
{$ELSE}
    true
{$ENDIF};

var
  MainSeed: Integer;
  MainTriangles: TTriangles;
  Transforms: Integer;
  EnableFinalXform: boolean;
  AppPath: string;
  OpenFile: string;
  CanDrawOnResize: boolean;
  PreserveWeights: boolean;
  AlwaysCreateBlankFlame: boolean;
  StartupCheckForUpdates: boolean;
  TBWidth1: Integer;
  TBWidth2: Integer;
  TBWidth3: Integer;
  TBWidth4: Integer;
  TBWidth5: Integer;
  ThumbnailPlaceholder: TBitmap;
  WarnOnMissingPlugin: boolean;
  LanguageFile: string;
  AvailableLanguages: TStringList;
  PluginPath: string;
  MultithreadedPreview: boolean;
  cmap_index: Integer;
  ImageFolder: string;

  UseFlameBackground, UseTransformColors: boolean;
  HelpersEnabled: boolean;
  EditorBkgColor, ReferenceTriangleColor: Integer;
  GridColor1, GridColor2, HelpersColor: Integer;
  ExtEditEnabled, TransformAxisLock, RebuildXaosLinks: boolean;
  ShowAllXforms: boolean;
  EditorPreviewTransparency: Integer;
  EnableEditorPreview: boolean;

  defSampleDensity, defPreviewDensity: double;
  defGamma, defBrightness, defVibrancy, defFilterRadius,
    defGammaThreshold: double;
  defOversample: Integer;

  renderDensity, renderFilterRadius: double;
  renderOversample, renderWidth, renderHeight: Integer;
  renderBitsPerSample: Integer;
  renderPath: string;
  JPEGQuality: Integer;
  renderFileFormat: Integer;
  InternalBitsPerSample: Integer;

  NrTreads: Integer;
  UseNrThreads: Integer;

  PNGTransparency: Integer;
  ShowTransparency: boolean;

  MainPreviewScale: double;
  ExtendMainPreview: boolean;

  StoreEXIF: boolean;
  StoreParamsEXIF: boolean;
  ExifAuthor: string;

  LastOpenFile: string;
  RememberLastOpenFile: boolean;
  UseSmallThumbnails: boolean;
  ConfirmDelete: boolean;
  ConfirmExit: boolean;
  ConfirmStopRender: boolean;
  SavePath, SmoothPalettePath: string;
  RandomPrefix, RandomDate: string;
  RandomIndex: Integer;
  FlameFile, GradientFile, GradientEntry, FlameEntry: string;
  ParamFolder: string;
  prevLowQuality, prevMediumQuality, prevHighQuality: double;
  defSmoothPaletteFile: string;
  BrowserPath: string;
  EditPrevQual, MutatePrevQual, AdjustPrevQual: Integer;
  randMinTransforms, randMaxTransforms: Integer;
  mutantMinTransforms, mutantMaxTransforms: Integer;
  KeepBackground: boolean;
  defFlameFile: string;
  HelpPath: string;

  PlaySoundOnRenderComplete: boolean;
  RenderCompleteSoundFile: string;

  SaveIncompleteRenders: boolean;
  ShowRenderStats: boolean;
  LowerRenderPriority: boolean;

  SymmetryType: Integer;
  SymmetryOrder: Integer;
  SymmetryNVars: Integer;
  Variations: array of boolean;

  MainForm_RotationMode: Integer;
  PreserveQuality: boolean;

  BatchSize: Integer;
  Favorites: TStringList;
  Script: string;
  ScriptPath: string;

  ShowProgress: boolean;
  defLibrary: string;
  LimitVibrancy: boolean;
  DefaultPalette: TColorMap;

  AutoOpenLog: boolean;
  AutoSaveEnabled: boolean;
  AutoSaveFreq: Integer;
  AutoSavePath: string;

  LineCenterColor: Integer;
  LineThirdsColor: Integer;
  LineGRColor: Integer;
  EnableGuides: boolean;

function Round6(x: double): double;

implementation

function WinExecAndWait32(FileName: string): integer;
var
  zAppName: array[0..1024] of Char;
  zCurDir: array[0..255] of Char;
  WorkDir: string;
  StartupInfo: TStartupInfo;
  ProcessInfo: TProcessInformation;
  r : dword;
begin
  StrPCopy(zAppName, FileName);
  GetDir(0, WorkDir);
  StrPCopy(zCurDir, WorkDir);
  FillChar(StartupInfo, Sizeof(StartupInfo), #0);
  StartupInfo.cb := Sizeof(StartupInfo);

  StartupInfo.dwFlags := STARTF_USESHOWWINDOW;
  StartupInfo.wShowWindow := 0;
  if (not CreateProcess(nil, zAppName, nil, nil, false, CREATE_NEW_CONSOLE or NORMAL_PRIORITY_CLASS, nil, nil, StartupInfo, ProcessInfo)) then
    Result := -1
  else begin
    WaitforSingleObject(ProcessInfo.hProcess, INFINITE);
    GetExitCodeProcess(ProcessInfo.hProcess, r);
    result := r;
    CloseHandle(ProcessInfo.hProcess);
    CloseHandle(ProcessInfo.hThread);
  end;
end;

function ScaleRect(r: TRect; scale: double): TSRect;
begin
  Result.Left := r.Left * scale;
  Result.Top := r.Top * scale;
  Result.Right := r.Right * scale;
  Result.Bottom := r.Bottom * scale;
end;

procedure swapcolor(var clist: array of cardinal; i, j: integer);
var
  t: cardinal;
begin
  t := clist[j];
  clist[j] := clist[i];
  clist[i] := t;
end;

function diffcolor(clist: array of cardinal; i, j: integer): cardinal;
var
  r1, g1, b1, r2, g2, b2: byte;
begin
  r1 := clist[j] and 255;
  g1 := clist[j] shr 8 and 255;
  b1 := clist[j] shr 16 and 255;
  r2 := clist[i] and 255;
  g2 := clist[i] shr 8 and 255;
  b2 := clist[i] shr 16 and 255;
  Result := abs((r1 - r2) * (r1 - r2)) + abs((g1 - g2) * (g1 - g2)) +
    abs((b1 - b2) * (b1 - b2));
end;

function EntryExists(En, Fl: string): boolean;
{ Searches for existing identifier in parameter files }
var
  FStrings: TStringList;
  i: integer;
begin
  Result := False;
  if FileExists(Fl) then
  begin
    FStrings := TStringList.Create;
    try
      FStrings.LoadFromFile(Fl);
      for i := 0 to FStrings.Count - 1 do
        if Pos(LowerCase(En) + ' {', Lowercase(FStrings[i])) <> 0 then
          Result := True;
    finally
      FStrings.Free;
    end
  end
  else
    Result := False;
end;

function DeleteEntry(Entry, FileName: string): boolean;
{ Deletes an entry from a multi-entry file }
var
  Strings: TStringList;
  p, i: integer;
begin
  Result := True;
  Strings := TStringList.Create;
  try
    i := 0;
    Strings.LoadFromFile(FileName);
    while Pos(Entry + ' ', Trim(Strings[i])) <> 1 do
    begin
      inc(i);
    end;
    repeat
      p := Pos('}', Strings[i]);
      Strings.Delete(i);
    until p <> 0;
    if (i < Strings.Count) and (Trim(Strings[i]) = '') then Strings.Delete(i);
    Strings.SaveToFile(FileName);
  finally
    Strings.Free;
  end;
end;

function CleanUPRTitle(ident: string): string;
{ Strips braces but leave spaces }
var
  i: integer;
begin
  for i := 1 to Length(ident) do
  begin
    if ident[i] = '}' then
      ident[i] := '_'
    else if ident[i] = '{' then
      ident[i] := '_';
  end;
  Result := ident;
end;


function CleanIdentifier(ident: string): string;
{ Strips unwanted characters from an identifier}
var
  i: integer;
begin
  for i := 0 to Length(ident) do
  begin
    if ident[i] = #32 then
      ident[i] := '_'
    else if ident[i] = '}' then
      ident[i] := '_'
    else if ident[i] = '{' then
      ident[i] := '_';
  end;
  Result := ident;
end;

function GetWinVersion: TWin32Version;
{ Returns current version of a host Win32 platform }
begin
  Result := wvUnknown;
  if Win32Platform = VER_PLATFORM_WIN32_WINDOWS then
    if (Win32MajorVersion > 4) or ((Win32MajorVersion = 4) and (Win32MinorVersion > 0)) then
      Result := wvWin98
    else
      Result := wvWin95
  else
    if Win32MajorVersion <= 4 then
      Result := wvWinNT
    else if Win32MajorVersion = 5 then
      if Win32MinorVersion = 0 then
        Result := wvWin2000
      else if Win32MinorVersion >= 1 then
        Result := wvWinXP
    else if Win32MajorVersion = 6 then
      if Win32MinorVersion = 0 then
        Result := wvWinVista
      else if Win32MinorVersion >= 1 then
        Result := wvWin7
    else if Win32MajorVersion >= 7 then
      Result := wvWinFutureFromOuterSpace;
end;

function WinShellExecute(const Operation, AssociatedFile: string): Boolean;
var
  a1: string;
  r: Cardinal;
begin
  a1 := Operation;
  if a1 = '' then
    a1 := 'open';

  r := ShellExecute(
    application.handle
    , pchar(a1)
    , pchar(AssociatedFile)
    , ''
    , ''
    , SW_SHOWNORMAL
    );
  if (r > 32) then WinShellExecute := true
  else WinShellExecute := false;
end;

function WinShellOpen(const AssociatedFile: string): boolean;
begin
  Result := WinShellExecute('open', AssociatedFile);
end;

function GradientString(c: TColorMap): string;
var
  strings: TStringList;
  i, j, cl: integer;
begin
  strings := TStringList.Create;
  for i := 0 to 255 do
  begin
    j := round(i * (399 / 255));
    cl := (c[i][2] shl 16) + (c[i][1] shl 8) + (c[i][0]);
    strings.Add(' index=' + IntToStr(j) + ' color=' + intToStr(cl));
  end;
  Result := Strings.Text;
  strings.Free;
end;


function GradTitle(str: string): string;
var
  p: integer;
begin
  p := pos('{', str);
  GradTitle := Trim(copy(str, 1, p - 1));
end;

function GetEnvVarValue(const VarName: string): string;
var
  BufSize: Integer;
begin
  BufSize := GetEnvironmentVariable(PChar(VarName), nil, 0);
  if BufSize > 0 then
  begin
    SetLength(Result, BufSize - 1);
    GetEnvironmentVariable(PChar(VarName), PChar(Result), BufSize);
  end
  else
    Result := '';
end;

function det(a, b, c, d: double): double;
begin
  Result := (a * d - b * c);
end;

function Round6(x: double): double;
begin
  Result := RoundTo(x, -6);
end;

function solve3(x1, x2, x1h, y1, y2, y1h, z1, z2, z1h: double;
  var a, b, e: double): double;
var
  det1: double;
begin
  det1 := x1 * det(y2, 1.0, z2, 1.0) - x2 * det(y1, 1.0, z1, 1.0) + 1 *
    det(y1, y2, z1, z2);
  if (det1 = 0.0) then
  begin
    Result := det1;
    EXIT;
  end
  else
  begin
    a := (x1h * det(y2, 1.0, z2, 1.0) - x2 * det(y1h, 1.0, z1h, 1.0) + 1 *
      det(y1h, y2, z1h, z2)) / det1;
    b := (x1 * det(y1h, 1.0, z1h, 1.0) - x1h * det(y1, 1.0, z1, 1.0) + 1 *
      det(y1, y1h, z1, z1h)) / det1;
    e := (x1 * det(y2, y1h, z2, z1h) - x2 * det(y1, y1h, z1, z1h) + x1h *
      det(y1, y2, z1, z2)) / det1;
    a := Round6(a);
    b := Round6(b);
    e := Round6(e);
    Result := det1;
  end;
end;

function dist(x1, y1, x2, y2: double): double;
begin
  Result := Hypot(x2 - x1, y2 - y1);
end;

function line_dist(x, y, x1, y1, x2, y2: double): double;
var
  a, b, e, c: double;
begin
  if ((x = x1) and (y = y1)) then
    a := 0.0
  else
    a := sqrt((x - x1) * (x - x1) + (y - y1) * (y - y1));
  if ((x = x2) and (y = y2)) then
    b := 0.0
  else
    b := sqrt((x - x2) * (x - x2) + (y - y2) * (y - y2));
  if ((x1 = x2) and (y1 = y2)) then
    e := 0.0
  else
    e := sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2));
  if ((a * a + e * e) < (b * b)) then
    Result := a
  else if ((b * b + e * e) < (a * a)) then
    Result := b
  else if (e <> 0.0) then
  begin
    c := (b * b - a * a - e * e) / (-2 * e);
    if ((a * a - c * c) < 0.0) then
      Result := 0.0
    else
      Result := sqrt(a * a - c * c);
  end
  else
    Result := a;
end;

function transform_affine(const t: TTriangle;
  const Triangles: TTriangles): boolean;
var
  ra, rb, rc, a, b, c: double;
begin
  Result := true;
  ra := dist(Triangles[-1].y[0], Triangles[-1].x[0], Triangles[-1].y[1],
    Triangles[-1].x[1]);
  rb := dist(Triangles[-1].y[1], Triangles[-1].x[1], Triangles[-1].y[2],
    Triangles[-1].x[2]);
  rc := dist(Triangles[-1].y[2], Triangles[-1].x[2], Triangles[-1].y[0],
    Triangles[-1].x[0]);
  a := dist(t.y[0], t.x[0], t.y[1], t.x[1]);
  b := dist(t.y[1], t.x[1], t.y[2], t.x[2]);
  c := dist(t.y[2], t.x[2], t.y[0], t.x[0]);
  if (a > ra) then
    Result := false
  else if (b > rb) then
    Result := false
  else if (c > rc) then
    Result := false
  else if ((a = ra) and (b = rb) and (c = rc)) then
    Result := false;
end;

function triangle_area(t: TTriangle): double;
var
  base, Height: double;
begin
  try
    base := dist(t.x[0], t.y[0], t.x[1], t.y[1]);
    Height := line_dist(t.x[2], t.y[2], t.x[1], t.y[1], t.x[0], t.y[0]);
    if (base < 1.0) then
      Result := Height
    else if (Height < 1.0) then
      Result := base
    else
      Result := 0.5 * base * Height;
  except
    on e: EMathError do
      Result := 0;
  end;
end;

function GetVal(token: string): string;
var
  p: Integer;
begin
  p := Pos('=', token);
  Delete(token, 1, p);
  Result := token;
end;

function ReplaceTabs(str: string): string;
var
  i: Integer;
begin
  for i := 1 to Length(str) do
  begin
    if str[i] = #9 then
    begin
      Delete(str, i, 1);
      Insert(#32, str, i);
    end;
  end;
  Result := str;
end;

function ReplaceStr(str, SearchStr, ReplaceStr: string): string;
begin
  while Pos(SearchStr, str) <> 0 do
  begin
    Insert(ReplaceStr, str, Pos(SearchStr, str));
    system.Delete(str, Pos(SearchStr, str), Length(SearchStr));
  end;
  Result := str;
end;

function SplitFilter(const fText: String; const fSep: Char;
  fTrim: boolean = false; fQuotes: boolean = false): TStringList;
var
  vI: Integer;
  vBuffer: String;
  vOn: boolean;
begin
  Result := TStringList.Create;
  vBuffer := '';
  vOn := true;
  for vI := 1 to Length(fText) do
  begin
    if (fQuotes and (fText[vI] = fSep) and vOn) or
      (Not(fQuotes) and (fText[vI] = fSep)) then
    begin
      if fTrim then
        vBuffer := Trim(vBuffer);
      if vBuffer = '' then
        vBuffer := fSep;
      if vBuffer[1] = fSep then
        vBuffer := Copy(vBuffer, 2, Length(vBuffer));
      Result.Add(vBuffer);
      vBuffer := '';
    end;
    if fQuotes then
    begin
      if fText[vI] = '"' then
      begin
        vOn := Not(vOn);
        Continue;
      end;
      if (fText[vI] <> fSep) or ((fText[vI] = fSep) and (vOn = false)) then
        vBuffer := vBuffer + fText[vI];
    end
    else if fText[vI] <> fSep then
      vBuffer := vBuffer + fText[vI];
  end;
  if vBuffer <> '' then
  begin
    if fTrim then
      vBuffer := Trim(vBuffer);
    Result.Add(vBuffer);
  end;
end;

function OpenSaveFileDialog(Parent: TWinControl;
  const DefExt, Filter, InitialDir, Title: string; var FileName: string;
  MustExist, OverwritePrompt, NoChangeDir, DoOpen: boolean): boolean;
var
  ofn: TOpenFileName;
  szFile: array [0 .. 260] of Char;
  fa, fa2: TStringList;
  h, i, j, k, c: Integer;
  cs, s: string;
begin
  Result := false;
  FillChar(ofn, SizeOf(TOpenFileName), 0);
  with ofn do
  begin
    lStructSize := SizeOf(TOpenFileName);
    hwndOwner := Parent.Handle;
    lpstrFile := szFile;
    nMaxFile := SizeOf(szFile);
    if (Title <> '') then
      lpstrTitle := PChar(Title);
    if (InitialDir <> '') then
      lpstrInitialDir := PChar(InitialDir);
    StrPCopy(lpstrFile, FileName);
    lpstrFilter := PChar(ReplaceStr(Filter, '|', #0) + #0#0);
    fa := SplitFilter(Filter, '|');

    k := 0;
    c := (fa.Count div 2);
    for i := 0 to c - 1 do
    begin
      j := 2 * i + 1;
      cs := LowerCase(fa.Strings[j]);
      fa2 := SplitFilter(cs, ';');
      for h := 0 to fa2.Count - 1 do
      begin
        cs := fa2.Strings[h];
        s := '*.' + LowerCase(DefExt);
        if (cs = s) then
          k := i;
      end;
    end;

    nFilterIndex := k + 1;
    if DefExt <> '' then
      lpstrDefExt := PChar(DefExt);
  end;

  if MustExist then
    ofn.Flags := ofn.Flags or OFN_FILEMUSTEXIST;
  if OverwritePrompt then
    ofn.Flags := ofn.Flags or OFN_OVERWRITEPROMPT;
  if NoChangeDir then
    ofn.Flags := ofn.Flags or OFN_NOCHANGEDIR;

  if DoOpen then
  begin
    if GetOpenFileName(ofn) then
    begin
      Result := true;
      FileName := StrPas(szFile);
    end;
  end
  else
  begin
    if GetSaveFileName(ofn) then
    begin
      Result := true;
      FileName := StrPas(szFile);
    end;
  end
end;

end.
