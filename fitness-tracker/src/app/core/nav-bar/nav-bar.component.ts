import { Component, EventEmitter, Input, Output } from "@angular/core";

@Component({
    selector:'core-nav-bar',
    templateUrl:'./nav-bar.component.html',
    styleUrls:['./nav-bar.component.css']
})
export class NavBarComponent{
    isExpanded = false;
    @Input() title:string;
    @Input() loggedIn: boolean;
    @Output() loginClicked = new EventEmitter<void>();
    @Output() logoutClicked = new EventEmitter<void>();

    collapse() {
        this.isExpanded = false;
    }
    
    toggle() {
        this.isExpanded = !this.isExpanded;
    }
    
    login() {
        this.loginClicked.emit();
    }
    
    logout() {
        this.logoutClicked.emit();
    }
}