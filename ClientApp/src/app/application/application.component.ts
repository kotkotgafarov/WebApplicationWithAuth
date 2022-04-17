import { Component, OnInit, Output, EventEmitter, Inject } from '@angular/core';
import { HttpEventType, HttpClient, HttpParams } from '@angular/common/http';
import { DataService } from '../services/data.service';
import { Enrollee } from '../models/enrollee';
import { getBaseUrl } from '../../main';
import { MatAutocompleteTrigger } from '@angular/material/autocomplete';
import { ViewChild } from '@angular/core';
import { Application } from '../models/application';
import { MatDialogModule, MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Competition } from '../models/competition';
import { ApplicationsExam } from '../models/applicationsexam';
import { EnrolleesDoc } from '../models/enrolleesdoc';
import { Subject } from 'rxjs';
import { ApplicationsContract } from '../models/applicationscontract';
import { ApplicationsAchievement } from '../models/applicationsachievement';
import { LKAUser } from '../models/lkauser';
import { ActivatedRoute } from '@angular/router';

export interface DialogData {
  code: string;
  speciality: string;
  description: string;
}

export class Subject0 {
  id: number;
  name: string;
}

@Component({
  selector: 'application',
  templateUrl: './application.component.html',
  providers: [DataService]
})
export class ApplicationComponent implements OnInit {

  enrollee: Enrollee = new Enrollee();
  enrolleeId: number;
  applicationscontract: ApplicationsContract = new ApplicationsContract();
  applicationsachievement: ApplicationsAchievement = new ApplicationsAchievement();
  application: Application;
  currentae: ApplicationsExam;
  docSelected: EnrolleesDoc;
  subjectSelected: Subject0;
  subjects: Array<Subject0> = [];
  lkauser: LKAUser;
  public currenttype: string;
  //addresses: Array<string> = [];

  constructor(private dataService: DataService, private http: HttpClient, public dialog: MatDialog, private route: ActivatedRoute) { }

  ngOnInit() {
    this.enrolleeId = Number(this.route.snapshot.paramMap.get('id'));
    this.loadData();
  }

  loadData() {
    this.dataService.getApplication(this.enrolleeId)
      .subscribe((data: Application) => this.application = data);

    this.dataService.getCurrentUser()
      .subscribe((data: LKAUser) => this.lkauser = data);
  }

  save() {
    this.dataService.updateApplication(this.application)
      .subscribe(() => this.loadData());
  }

  updatestatus(status: number) {
    this.application.enrolleesstatus.applicationStatus = status;
    this.dataService.updateEnrolleesStatus(this.application.enrollee, this.application.enrolleesstatus)
      .subscribe(() => true);
  }

  openDescription(c: Competition) {
    const dialogRef = this.dialog.open(DialogApplicationComponent, {
      width: '50%', height: '50%',
      data: { code: c.code, speciality: c.speciality, description: c.description },
    });
    // const dialogRef = this.dialog.open(DialogApplicationComponent);

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
      //this.animal = result;
    });
  }

  // -------------- do with exams ------------------
  saveExam() {
    this.application.applicationsexam.forEach((value, index) => {
      if (value.id == this.currentae.id) {
        value.type = this.currentae.type;
        value.score = this.currentae.score;
        value.subjectsSubstitutorId = this.currentae.subjectsSubstitutorId;
        value.subjectsSubstitutorName = this.currentae.subjectsSubstitutorName;
        value.enrolleesDocId = this.currentae.enrolleesDocId;
        if (value.type == 2) {
          value.enrolleesDocId = 0;
          value.enrolleesDocName = '';
        }
      };
    });
    this.currentae = new ApplicationsExam();
  }

  editExam(ae: ApplicationsExam) {
    if (ae.enrolleesDocId) {
      this.docSelected = this.application.docs.find(x => x.id == ae.enrolleesDocId);
    }
    this.currentae = ae;
    this.currenttype = "" + ae.type;


    this.subjects = [];
    var _subject = new Subject0;
    _subject.id = ae.competitionsExamId;
    _subject.name = ae.subjectName;
    this.subjects.push(_subject);

    if (_subject.id == ae.subjectId && ae.subjectsSubstitutorId == 0) {
      this.subjectSelected = _subject;
    }

    this.application.subjectsubstitutors.forEach((value, index) => {
      if (value.subjectId == ae.subjectId) {
        _subject = new Subject0;
        _subject.id = value.subjectsSubstitutorId;
        _subject.name = value.subjectsSubstitutorName;
        this.subjects.push(_subject);
        if (_subject.id == ae.subjectsSubstitutorId) {
          this.subjectSelected = _subject;
        }
      }
    })
  }
  cancelChangesExam() {
    this.currentae = new ApplicationsExam();
  }

  onDocChange(i: number) {
    if (this.currentae && i == 1) {
      this.currentae.enrolleesDocId = this.docSelected.id;
      this.currentae.enrolleesDocName = this.docSelected.type + " " + this.docSelected.series + this.docSelected.number;
    }
    if (this.applicationscontract && i == 2) {
      this.applicationscontract.enrolleesDocId = this.docSelected.id;
      this.applicationscontract.enrolleesDocName = this.docSelected.type + " " + this.docSelected.series + this.docSelected.number;
    }
    if (this.applicationsachievement && i == 3) {
      this.applicationsachievement.enrolleesDocId = this.docSelected.id;
      this.applicationsachievement.enrolleesDocName = this.docSelected.type + " " + this.docSelected.series + this.docSelected.number;
    }
  }
  onCurrentTypeChange() {
    this.currentae.type = +this.currenttype;
    if (this.currentae.type != 1) {
      this.currentae.score = 0;
    }
  }

  onSubjectChange() {
    if (this.currentae.subjectId != this.subjectSelected.id) {
      this.currentae.subjectsSubstitutorId = this.subjectSelected.id;
      this.currentae.subjectsSubstitutorName = this.subjectSelected.name;
    }
    else {
      this.currentae.subjectsSubstitutorId = 0;
      this.currentae.subjectsSubstitutorName = '';
    }
  }

  // -------------- do with contracts ------------------
  getmaximalidcontract() {
    var maxid = 1;
    this.application.applicationscontract.forEach((value, index) => {
      if (value.id > maxid) maxid = value.id;
    });
    return maxid;
  }

  saveContract() {
    this.applicationscontract = new ApplicationsContract();
  }

  editContract(d: ApplicationsContract) {
    this.applicationscontract = d;
  }

  deleteContract(d: ApplicationsContract) {
    this.application.applicationscontract.forEach((value, index) => {
      if (value.id == d.id) this.application.applicationscontract.splice(index, 1);
    });
  }
  addContract() {
    this.applicationscontract = new ApplicationsContract();
    this.applicationscontract.id = this.getmaximalidcontract() + 1;
    this.applicationscontract.enrolleeId = this.application.enrollee.id;
    this.application.applicationscontract.push(this.applicationscontract);
  }

  // -------------- do with achievements ------------------
  getmaximalidachievements() {
    var maxid = 1;
    this.application.applicationsachievement.forEach((value, index) => {
      if (value.id > maxid) maxid = value.id;
    });
    return maxid;
  }

  saveAchievement() {
    this.applicationsachievement = new ApplicationsAchievement();
  }

  editAchievement(d: ApplicationsAchievement) {
    this.applicationsachievement = d;
  }

  deleteAchievement(d: ApplicationsAchievement) {
    this.application.applicationsachievement.forEach((value, index) => {
      if (value.id == d.id) this.application.applicationsachievement.splice(index, 1);
    });
  }
  addAchievement() {
    this.applicationsachievement = new ApplicationsAchievement();
    this.applicationsachievement.id = this.getmaximalidachievements() + 1;
    this.applicationsachievement.enrolleeId = this.application.enrollee.id;
    this.application.applicationsachievement.push(this.applicationsachievement);
  }
}



@Component({
  selector: 'dialog_application',
  templateUrl: './dialog_application.component.html',
})
export class DialogApplicationComponent{
  constructor(public dialogRef: MatDialogRef<DialogApplicationComponent>, @Inject(MAT_DIALOG_DATA) public data: DialogData) { }

  close(): void {
    this.dialogRef.close();
  }
}
