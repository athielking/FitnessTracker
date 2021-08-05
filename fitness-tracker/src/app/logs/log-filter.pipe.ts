import { Pipe, PipeTransform } from '@angular/core';
import { from, of, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { ILog, Log } from '../shared/models/log';
import { LogsModule } from './logs.module';

@Pipe({ name: 'logFilter' })
export class LogFilterPipe implements PipeTransform {

  transform(items: Observable<ILog[]>, searchText: string): Observable<ILog[]> {
    if (!items) {
      return of([]);
    }
    if (!searchText) {
      return items;
    }
    searchText = searchText.toLocaleLowerCase();

    return items.pipe(map(logs => logs.filter(log => log.user.userName.toLocaleLowerCase().includes(searchText))));
  }
}