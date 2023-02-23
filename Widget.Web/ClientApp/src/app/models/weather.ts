export interface Weather {
  location: Location;
  current: Current;
}

export interface Location {
  name: string;
  region: string;
}

export interface Condition {
  text: string;
  icon: string;
  code: string;
}

export interface Current {
  last_updated: string;
  temp_f: string;
  condition: Condition;
  wind_mph: string;
  wind_dir: string;
  humidity: string;
}
