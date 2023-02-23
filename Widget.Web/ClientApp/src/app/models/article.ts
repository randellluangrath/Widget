export interface Article {
  source: Source;
  title: string;
  description: string;
  urlToImage: string;
}

export interface Source {
  id: string;
  name: string;
}
