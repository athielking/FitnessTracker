import { Component, OnInit } from '@angular/core';

import { AuthStore } from '../auth.store';

@Component({
    selector: 'app-callback',
    template: `
      <p>
        Loading...
      </p>
    `,
    styles: []
  })
export class AuthLoadingComponent{
    
    constructor(private authStore: AuthStore) { }
}