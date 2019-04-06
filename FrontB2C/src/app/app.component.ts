import { Component } from '@angular/core';
import { MessageComponent } from './component/message/message.component';
import {MatDialog} from '@angular/material';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'turesbalon';


  constructor(public dialog: MatDialog) {
    this.OnInit();
  }

  OnInit(){
  }

  openDialog( icon,msg,title): void {
    const dialogRef = this.dialog.open(MessageComponent, {
      width: '250px',
      data: {icon: icon, msg: msg, title: title}
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
    });
  }
}
