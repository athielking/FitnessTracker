export function patternValidator(regex: RegExp, str:string):boolean{
    const valid = regex.test(str);
    return valid;
}