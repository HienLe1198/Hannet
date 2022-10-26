import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class UrlApiService {

  constructor() { }

  getUrlApiDatabse(): string {
    let filePath: string = '../../../assets/config/config_url_api.json';
    let result: any;
    var rawFile = new XMLHttpRequest();
    rawFile.open("GET", filePath, false);
    rawFile.onreadystatechange = function () {
      if (rawFile.readyState === 4) {
        if (rawFile.status === 200 || rawFile.status == 0) {
          result =  JSON.parse(rawFile.responseText).url_api_database+'api/kioskmanagement/';
        }
      }
    }
    rawFile.send(null);
    return result;
  }
  
  getUrlApiDevice(): string {
    let filePath: string = '../../../assets/config/config_url_api.json';
    let result: any;
    var rawFile = new XMLHttpRequest();
    rawFile.open("GET", filePath, false);
    rawFile.onreadystatechange = function () {
      if (rawFile.readyState === 4) {
        if (rawFile.status === 200 || rawFile.status == 0) {
          result =  JSON.parse(rawFile.responseText).url_api_device+'api/kioskmanagement/';
        }
      }
    }
    rawFile.send(null);
    return result;
  }
}
