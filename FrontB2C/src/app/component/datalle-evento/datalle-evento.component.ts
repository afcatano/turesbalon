import { Component, Inject } from '@angular/core';
import {MatDialog, MatDialogRef, MAT_DIALOG_DATA} from '@angular/material';
import { DialogData } from '../../Models/messageDialog';

@Component({
  selector: 'app-datalle-evento',
  templateUrl: './datalle-evento.component.html',
  styleUrls: ['./datalle-evento.component.css']
})
export class DatalleEventoComponent  {

  constructor(
    public dialogRef: MatDialogRef<DatalleEventoComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any) {
    }

  onNoClick(): void {
    this.dialogRef.close();
  }



}
