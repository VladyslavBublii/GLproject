import { Component } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { LanguageService } from './language/language.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  constructor(translate: TranslateService, private languageService: LanguageService) {
    translate.setDefaultLang(languageService.getCurrentLanguage());
    translate.use(languageService.getCurrentLanguage());
  }
  title = 'Web-Shop';
}
