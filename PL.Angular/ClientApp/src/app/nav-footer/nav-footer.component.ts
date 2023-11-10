import { Component } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-nav-footer',
  templateUrl: './nav-footer.component.html',
  styleUrls: ['./nav-footer.component.css']
})
export class NavFooterComponent {
  constructor(private translate: TranslateService) {
    translate.setDefaultLang('ua');
    translate.use('ua'); 
  }
}
