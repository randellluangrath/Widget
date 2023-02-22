export interface Weather {
  Location: Location;
  Current: Current;
}

export interface Location {}

export interface Condition {
  Text: string;
  Icon: string;
  Code: number;
}

export interface Current {
  LastUpdatedEpoch: number;
  LastUpdated: string;
  TempC: number;
  TempF: number;
  IsDay: number;
  Condition: Condition;
  WindMph: number;
  WindKph: number;
  WindDegree: number;
  WindDir: string;
  PressureMb: number;
  PressureIn: number;
  PrecipMm: number;
  PrecipIn: number;
  Humidity: number;
  Cloud: number;
  FeelslikeC: number;
  FeelslikeF: number;
  VisKm: number;
  VisMiles: number;
  Uv: number;
  GustMph: number;
  GustKph: number;
}
