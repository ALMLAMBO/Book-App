import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable()
export class AuthorService {
  private headers: HttpHeaders;
  private accessPointUrl: string = 'http://localhost:9191/api/authors';

  constructor(private http: HttpClient) {
    this.headers = new HttpHeaders({ 'Content-Type': 'application/json; charset=utf-8'});
  }

  public get() {
    return this.http
      .get(this.accessPointUrl, { headers: this.headers });
  }

  public add(author) {
    return this.http
      .post(this.accessPointUrl, author, { headers: this.headers });
  }

  public remove(author) {
    return this.http
      .delete(this.accessPointUrl + '/' + author.id, { headers: this.headers });
  }

  public update(author) {
    return this.http
      .put(this.accessPointUrl + '/' + author.id, author, { headers: this.headers });
  }
}
