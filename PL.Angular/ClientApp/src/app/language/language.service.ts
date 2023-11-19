import { Injectable } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

@Injectable({
  providedIn: 'root',
})
export class LanguageService {
    constructor(private translate: TranslateService) 
    {
        translate.setDefaultLang(this.currentLanguage);
        translate.use(this.currentLanguage);
    }

    private currentLanguage = "ua"

    ngOnInit(): void {
        this.translate.setDefaultLang(this.currentLanguage);
        this.translate.use(this.currentLanguage);
    }

    setCurrentLanguage(language: string): void {
        this.currentLanguage = language;
        this.ngOnInit();
    }

    getCurrentLanguage(){
        return this.currentLanguage;
    }
}
