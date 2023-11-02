import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

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
        @Inject(MAT_DIALOG_DATA) public data: DialogData) {}
    
    onOkClick(): void {
        this.dialogRef.close();
    }
  
  }