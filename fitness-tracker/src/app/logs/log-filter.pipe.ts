import { Pipe, PipeTransform } from '@angular/core';
import { Log } from '../shared/models/log';

@Pipe({ name: 'logFilter' })
export class LogFilterPipe implements PipeTransform {

  transform(items: Log[], searchText: string): any[] {
    if (!items) {
      return [];
    }
    if (!searchText) {
      return items;
    }
    searchText = searchText.toLocaleLowerCase();

    return items.filter(it => {
      return it.user.userName.toLocaleLowerCase().includes(searchText);
    });
  }
}