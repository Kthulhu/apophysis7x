unit varCurl3D;

interface

uses
  Variation, VariationPoolManager;

const
  variation_name = 'curl3D';
  num_vars = 3;
  var_cx_name = 'curl3D_cx';
  var_cy_name = 'curl3D_cy';
  var_cz_name = 'curl3D_cz';

type
  TVariationCurl3D = class(TVariation)
  private
    cx, cy, cz: double;

    cx2, cy2, cz2, c2,
    c2x, c2y, c2z: double;

    procedure CalcCx;
    procedure CalcCy;
    procedure CalcCz;
    procedure CalcLinear;

  public
    constructor Create;

    class function GetName: string; override;
    class function GetInstance: TVariation; override;

    function GetNrVariables: integer; override;
    function GetVariableNameAt(const Index: integer): string; override;

    function SetVariable(const Name: string; var value: double): boolean; override;
    function GetVariable(const Name: string; var value: double): boolean; override;
    function ResetVariable(const Name: string): boolean; override;

    procedure Prepare; override;
    procedure CalcFunction; override;
    procedure ObtainCalculateFunctionPtr(out f: TCalcFunction); override;
  end;

implementation

uses
  Math;

// TVariationCurl3D

///////////////////////////////////////////////////////////////////////////////
constructor TVariationCurl3D.Create;
var
  rnd: double;
begin
  rnd := 2*random - 1;

  // which maniac made this??
  {case random(3) of
    0: cx := rnd;
    1: cy := rnd;
    2: cz := rnd;
  end;}
  cy := 0; cy := 0; cz := 0;
end;

procedure TVariationCurl3D.Prepare;
begin
  c2x := 2 * cx;
  c2y := 2 * cy;
  c2z := 2 * cz;

  cx2 := sqr(cx);
  cy2 := sqr(cy);
  cz2 := sqr(cz);

  c2 := cx2 + cy2 + cz2;
end;

///////////////////////////////////////////////////////////////////////////////
procedure TVariationCurl3D.ObtainCalculateFunctionPtr(out f: TCalcFunction);
begin
{
  if IsZero(cx) and IsZero(cy) and IsZero(cz) then f := CalcLinear
  else
  if IsZero(cx) and IsZero(cy) then f := CalcCz
  else
  if IsZero(cy) and IsZero(cz) then f := CalcCx
  else
  if IsZero(cz) and IsZero(cx) then f := CalcCy
  else
  f := CalcFunction;
}
  if IsZero(cx) then begin
    if IsZero(cy) then begin
      if IsZero(cz) then
        f := CalcLinear
      else
        f := CalcCz;
    end
    else begin
      if IsZero(cz) then
        f := CalcCy
      else
        f := CalcFunction;
    end
  end
  else begin
    if IsZero(cy) and IsZero(cz) then
      f := CalcCx
    else
      f := CalcFunction;
  end;

  f := CalcFunction;

end;

///////////////////////////////////////////////////////////////////////////////
procedure TVariationCurl3D.CalcFunction;
var
  r, r2: double;
begin
  r2 := sqr(FTx^) + sqr(FTy^) + sqr(Ftz^);
  r := vvar / (r2*c2 + c2x*FTx^ - c2y*FTy^ + c2z*FTz^ + 1);

  FPx^ := FPx^ + r * (FTx^ + cx*r2);
  FPy^ := FPy^ + r * (FTy^ - cy*r2);
  FPz^ := FPz^ + r * (FTz^ + cz*r2);
end;

///////////////////////////////////////////////////////////////////////////////
procedure TVariationCurl3D.CalcCx;
var
  x, r, r2: double;
begin
  r2 := sqr(FTx^) + sqr(FTy^) + sqr(Ftz^);

  r := vvar / (cx2*r2 + c2x*FTx^ + 1);

  FPx^ := FPx^ + r * (FTx^ + cx*r2);
  FPy^ := FPy^ + r * FTy^;
  FPz^ := FPz^ + r * FTz^;
end;

///////////////////////////////////////////////////////////////////////////////
procedure TVariationCurl3D.CalcCy;
var
  r, r2: double;
begin
  r2 := sqr(FTx^) + sqr(FTy^) + sqr(Ftz^);

  r := vvar / (cy2*r2 - c2y*FTy^ + 1);

  FPx^ := FPx^ + r * FTx^;
  FPy^ := FPy^ + r * (FTy^ - cy*r2);
  FPz^ := FPz^ + r * FTz^;
end;

///////////////////////////////////////////////////////////////////////////////
procedure TVariationCurl3D.CalcCz;
var
  r, r2: double;
begin
  r2 := sqr(FTx^) + sqr(FTy^) + sqr(Ftz^);

  r := vvar / (cz2*r2 + c2z*FTz^ + 1);

  FPx^ := FPx^ + r * FTx^;
  FPy^ := FPy^ + r * FTy^;
  FPz^ := FPz^ + r * (FTz^ + cz*r2);
end;

///////////////////////////////////////////////////////////////////////////////
procedure TVariationCurl3D.CalcLinear;
var
  r: double;
begin
  FPx^ := FPx^ + vvar * FTx^;
  FPy^ := FPy^ + vvar * FTy^;
  FPz^ := FPz^ + vvar * FTz^;
end;

///////////////////////////////////////////////////////////////////////////////
class function TVariationCurl3D.GetInstance: TVariation;
begin
  Result := TVariationCurl3D.Create;
end;

///////////////////////////////////////////////////////////////////////////////
class function TVariationCurl3D.GetName: string;
begin
  Result := variation_name;
end;

///////////////////////////////////////////////////////////////////////////////
function TVariationCurl3D.GetVariableNameAt(const Index: integer): string;
begin
  case Index of
    0: Result := var_cx_name;
    1: Result := var_cy_name;
    2: Result := var_cz_name;
  else
    Result := '';
  end
end;

///////////////////////////////////////////////////////////////////////////////
function TVariationCurl3D.SetVariable(const Name: string; var value: double): boolean;
begin
  Result := False;
  if Name = var_cx_name then begin
    cx := value;
    Result := True;
  end
  else if Name = var_cy_name then begin
    cy := value;
    Result := True;
  end
  else if Name = var_cz_name then begin
    cz := value;
    Result := True;
  end;
end;

function TVariationCurl3D.ResetVariable(const Name: string): boolean;
begin
  Result := False;
  if Name = var_cx_name then begin
    cx := 0;
    Result := True;
  end
  else if Name = var_cy_name then begin
    cy := 0;
    Result := True;
  end
  else if Name = var_cz_name then begin
    cz := 0;
    Result := True;
  end;
end;

///////////////////////////////////////////////////////////////////////////////
function TVariationCurl3D.GetNrVariables: integer;
begin
  Result := num_vars;
end;

///////////////////////////////////////////////////////////////////////////////
function TVariationCurl3D.GetVariable(const Name: string; var value: double): boolean;
begin
  Result := False;
  if Name = var_cx_name then begin
    value := cx;
    Result := True;
  end
  else if Name = var_cy_name then begin
    value := cy;
    Result := True;
  end
  else if Name = var_cz_name then begin
    value := cz;
    Result := True;
  end;
end;

///////////////////////////////////////////////////////////////////////////////
initialization RegisterVariation(TIntegratedVariationLoader.Create(TVariationCurl3D, true, false)) end.
