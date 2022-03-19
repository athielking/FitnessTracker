import { AbstractControl, FormGroup } from '@angular/forms';
import { patternValidator } from '../string-helper';

export function passwordGuard(num:number) : {[key:string]: any} | null{

    return (ctrl:AbstractControl) => {
        const password:string = ctrl.value.toString();

        if( password.length >= num && patternValidator(/\d/, password) && 
            patternValidator(/[A-Z]/, password) && 
            patternValidator(/[a-z]/, password) &&
            patternValidator(/[!@#$%^&*()_+\-=\[\]{};':"\\|,.<>\/?]+/, password)){
            
            return null;        
        }

        return {passwordStrength: true}    
    }  
}

export function confirmedValidator(controlName1: string, controlName2:string){

    return (formGroup: FormGroup) => {
          const control1:AbstractControl = formGroup.controls[controlName1];
          const control2:AbstractControl  = formGroup.controls[controlName2];
        
          if (control2.errors && !control2.errors.match) {
              return;
          }
          
          if (control1.value !== control2.value) {
              return control2.setErrors({ confirmed: true });
          } else {
              return control2.setErrors(null);
          }
      }
  }

export function propertyLengthGuard(num:number, key:string): {[key:string]: any} | null{
    return (formGroup: FormGroup) => {
        const prop:string = formGroup ? formGroup.value : '';
  
        if(formGroup && (!formGroup.value || prop.length >= 2)){
            return null;
        }
            
        return {minLength : true };
    }
}

export function getControlName(control: AbstractControl):string{
    let controlName = null;
    let parent = control["_parent"];

    if (parent instanceof FormGroup)
    {
        Object.keys(parent.controls).forEach((name) =>
        {
            if (control === parent.controls[name])
            {
                controlName = name;
            }
        });
    }

    return controlName;
}