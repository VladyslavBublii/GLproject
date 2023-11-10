import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { TranslateService } from '@ngx-translate/core';

export interface DialogData {
    message: string;
}

@Component({
    selector: 'alert-dialog',
    templateUrl: './alert-dialog.component.html',
})
export class DialogAlertComponent {
    constructor(
        public dialogRef: MatDialogRef<DialogAlertComponent>,
        @Inject(MAT_DIALOG_DATA) public data: DialogData,
        private translate: TranslateService
        ) {
            translate.setDefaultLang('ua');
            translate.use('ua');
        }
    
    onOkClick(): void {
        this.dialogRef.close();
    }
  
  }