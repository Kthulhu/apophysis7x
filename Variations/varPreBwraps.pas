unit varPreBwraps;

interface

uses
  Variation, VariationPoolManager;

type
  TVariationPreBwraps = class(TVariation)
  private
    pre_bwraps_cellsize, pre_bwraps_space, pre_bwraps_gain,
    pre_bwraps_inner_twist, pre_bwraps_outer_twist,
    g2, r2, rfactor: double;
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
  end;

implementation

uses
  Math;
///////////////////////////////////////////////////////////////////////////////
procedure TVariationPreBwraps.Prepare;
var
  max_bubble, radius: double;
begin
  radius := 0.5 * (pre_bwraps_cellsize / (1.0 + sqr(pre_bwraps_space)));
  g2 := sqr(pre_bwraps_gain) / (radius + 1e-6) + 1e-6;
  max_bubble := g2 * radius;

  if (max_bubble > 2.0) then max_bubble := 1.0
  else max_bubble := max_bubble * (1.0 / (sqr(max_bubble)/4.0 + 1.0));

  r2 := sqr(radius);
  rfactor := radius / max_bubble;
end;

procedure TVariationPreBwraps.CalcFunction;
var
  Vx, Vy,
  Cx, Cy,
  Lx, Ly,
  r, theta, s, c : double;
begin
  Vx := FTx^;
  Vy := FTy^;

	if (pre_bwraps_cellsize <> 0.0) then
  begin
    Cx := (floor(Vx / pre_bwraps_cellsize) + 0.5) * pre_bwraps_cellsize;
    Cy := (floor(Vy / pre_bwraps_cellsize) + 0.5) * pre_bwraps_cellsize;

	  Lx := Vx - Cx;
	  Ly := Vy - Cy;

    if ((sqr(Lx) + sqr(Ly)) <= r2) then
    begin
      Lx := Lx * g2;
      Ly := Ly * g2;

      r := rfactor / ((sqr(Lx) + sqr(Ly)) / 4.0 + 1);
      
      Lx := Lx * r;
      Ly := Ly * r;

      r := (sqr(Lx) + sqr(Ly)) / r2;
      theta := pre_bwraps_inner_twist * (1.0 - r) + pre_bwraps_outer_twist * r;
      SinCos(theta, s, c);

      Vx := Cx + c * Lx + s * Ly;
      Vy := Cy - s * Lx + c * Ly;

      FTx^ := VVAR * Vx;
      FTy^ := VVAR * Vy;
      FTz^ := VVAR * FTz^;
    end;
  end;

end;

///////////////////////////////////////////////////////////////////////////////
constructor TVariationPreBwraps.Create;
begin
  pre_bwraps_cellsize := 1;
  pre_bwraps_space := 0;
  pre_bwraps_gain := 1;
  pre_bwraps_inner_twist := 0;
  pre_bwraps_outer_twist := 0;
end;

///////////////////////////////////////////////////////////////////////////////
class function TVariationPreBwraps.GetInstance: TVariation;
begin
  Result := TVariationPreBwraps.Create;
end;

///////////////////////////////////////////////////////////////////////////////
class function TVariationPreBwraps.GetName: string;
begin
  Result := 'pre_bwraps';
end;

///////////////////////////////////////////////////////////////////////////////
function TVariationPreBwraps.GetVariableNameAt(const Index: integer): string;
begin
  case Index Of
  0: Result := 'pre_bwraps_cellsize';
  1: Result := 'pre_bwraps_space';
  2: Result := 'pre_bwraps_gain';
  3: Result := 'pre_bwraps_inner_twist';
  4: Result := 'pre_bwraps_outer_twist';
  else
    Result := '';
  end
end;

///////////////////////////////////////////////////////////////////////////////
function TVariationPreBwraps.SetVariable(const Name: string; var value: double): boolean;
begin
  Result := False;
  if Name = 'pre_bwraps_cellsize' then begin
    pre_bwraps_cellsize := Value;
    Result := True;
  end else if Name = 'pre_bwraps_space' then begin
    pre_bwraps_space := Value;
    Result := True;
  end else if Name = 'pre_bwraps_gain' then begin
    pre_bwraps_gain := Value;
    Result := True;
  end else if Name = 'pre_bwraps_inner_twist' then begin
    pre_bwraps_inner_twist := Value;
    Result := True;
  end else if Name = 'pre_bwraps_outer_twist' then begin
    pre_bwraps_outer_twist := Value;
    Result := True;
  end 
end;
function TVariationPreBwraps.ResetVariable(const Name: string): boolean;
begin
  Result := False;
  if Name = 'pre_bwraps_cellsize' then begin
    pre_bwraps_cellsize := 1;
    Result := True;
  end else if Name = 'pre_bwraps_space' then begin
    pre_bwraps_space := 0;
    Result := True;
  end else if Name = 'pre_bwraps_gain' then begin
    pre_bwraps_gain := 1;
    Result := True;
  end else if Name = 'pre_bwraps_inner_twist' then begin
    pre_bwraps_inner_twist := 0;
    Result := True;
  end else if Name = 'pre_bwraps_outer_twist' then begin
    pre_bwraps_outer_twist := 0;
    Result := True;
  end;
end;

///////////////////////////////////////////////////////////////////////////////
function TVariationPreBwraps.GetNrVariables: integer;
begin
  Result := 5
end;

///////////////////////////////////////////////////////////////////////////////
function TVariationPreBwraps.GetVariable(const Name: string; var value: double): boolean;
begin
  Result := False;
  if Name = 'pre_bwraps_cellsize' then begin
    if Value = 0 then Value := 1e-6;
    Value := pre_bwraps_cellsize;
    Result := True;
  end else if Name = 'pre_bwraps_space' then begin
    Value := pre_bwraps_space;
    Result := True;
  end else if Name = 'pre_bwraps_gain' then begin
    Value := pre_bwraps_gain;
    Result := True;
  end else if Name = 'pre_bwraps_inner_twist' then begin
    Value := pre_bwraps_inner_twist;
    Result := True;
  end else if Name = 'pre_bwraps_outer_twist' then begin
    Value := pre_bwraps_outer_twist;
    Result := True;
  end
end;

///////////////////////////////////////////////////////////////////////////////
initialization RegisterVariation(TIntegratedVariationLoader.Create(TVariationPreBwraps, true, false)) end.
