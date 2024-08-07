import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { StorageService } from '../storage/storage.service';
import { LanguageService } from '../language/language.service';
import { UserRole } from '../models/enums/user-role.enum'; 

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit {
  isLoggedIn = false;
  isAdmin = false; 
  isExpanded = false;

  constructor(
    private http: HttpClient,
    private storageService: StorageService,
    private languageService: LanguageService
  ) {}

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  ngOnInit(): void {
    if (this.storageService.isLoggedIn()) {
      this.isLoggedIn = true;
      const userRole = this.storageService.getUserRole();
      this.isAdmin = userRole === UserRole.admin;
    }
  }

  logout(): void {
    this.storageService.clean();
    this.isLoggedIn = false;
    window.location.reload();
  }

  changeLanguage(language: string): void {
    this.languageService.setCurrentLanguage(language);
    console.log(`Selected language: ${language}`);
  }
}