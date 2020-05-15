import { Component, OnInit, Input } from '@angular/core';
import { Router } from '@angular/router';
import {ILog, ISingleLog, ILogExercise} from '../log'

@Component({
  selector: 'log-detail',
  templateUrl: './log-detail.component.html',
  styleUrls: ['./log-detail.component.css']
})
export class LogDetailComponent implements OnInit {
  
  @Input() log:ILog

  constructor() { }
  ngOnInit(): void {
    console.log(this.log.logId)
  }
}
