import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';

import { AuthStore } from './auth/auth.store';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css', '../../node_modules/ngx-toastr/toastr.css']
})
export class AppComponent  implements OnInit{
  
  authorized$: Observable<boolean>;
  
  loggedInUser: string;
  title:string = 'Fitness Tracker';

  constructor(private authStore:AuthStore){
      this.authorized$ = this.authStore.isLoggedIn$;
  }

  ngOnInit(): void {
      this.authStore.loggedInUser$.subscribe(user =>{
          this.loggedInUser = user && user.userName ? user.userName : ""
      })
  }
}
