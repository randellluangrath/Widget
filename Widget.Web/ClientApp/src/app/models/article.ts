export interface Article {
  Source: Source;
  Author: string;
  Title: string;
  Description: string;
  Url: string;
  UrlToImage: string;
  PublishedAt: Date | null;
  Content: string;
}

export interface Source {
  Id: string;
  Name: string;
}
