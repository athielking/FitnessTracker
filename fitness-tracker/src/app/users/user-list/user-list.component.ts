import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormControl, FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { Subject, Observable, combineLatest } from 'rxjs';
import { startWith, takeUntil, map } from 'rxjs/operators';

import { IUser } from 'src/app/shared/models/user';
import { UserService } from '../user.service';

@Component({
    templateUrl: "./user-list.component.html",
    styleUrls: ["../user.css"]
  })
export class UserListComponent implements OnInit, OnDestroy{
    
    users$: Observable<IUser[]>  
    filteredUsers$: Observable<IUser[]>
    filter$: Observable<string> 

    filter:FormControl
    userForm:FormGroup
    
    private _onDestroy$ = new Subject();

    constructor(private userService:UserService, private formBuilder: FormBuilder, private route: Router){}
    
    ngOnInit(){
        this.initFormGroup();
        this.users$ = this.userService.getUsers(); 
  
        this.filter$ = this.userForm.get('userFilter').valueChanges.pipe(startWith(''));
  
        this.filteredUsers$ = combineLatest(this.users$, this.filter$).pipe(takeUntil(this._onDestroy$),
            map(([users, filterString]) => users.filter(user => user.userName.toLowerCase().indexOf
            (filterString.toLowerCase()) !== -1 ))
        );
    }

    ngOnDestroy(): void {
        this._onDestroy$.next(null);
        this._onDestroy$.unsubscribe();
    }

    userLogs(user:IUser){
        this.route.navigate(['/logs-user', user.userName])
    }

    private initFormGroup(){
        this.userForm = this.formBuilder.group({
            userFilter:['']
        });
    }
}