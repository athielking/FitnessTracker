import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import {ILog} from './log'
import { LogService } from './log-list.service';

@Component({
  templateUrl: './log-detail.component.html',
  styleUrls: ['./log-detail.component.css']
})
export class LogDetailComponent implements OnInit {

  log:ILog = undefined
  pageTitle:string = "Log Details: "
  errorMessage:string = ""
  
  constructor(private route: ActivatedRoute, private router: Router, private logService:LogService) { }

  ngOnInit(): void {
    let id = +this.route.snapshot.paramMap.get('id');
    this.logService.getLogByid(id).subscribe({
      next:(log:ILog) => {
        this.log = log;
        this.pageTitle += `: ${id}`;
      },
      error:err => this.errorMessage = err
    })
  }

  onBack():void{
    this.router.navigate(['/logs']);
  }
}
