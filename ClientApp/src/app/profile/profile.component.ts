import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { HttpEventType, HttpClient, HttpParams } from '@angular/common/http';
import { DataService } from '../services/data.service';
import { Enrollee } from '../models/enrollee';
import { EnrolleesDoc } from '../models/enrolleesdoc';
import { Profile } from '../models/profile';
import { Relative } from '../models/relative';
import { DocType } from '../models/doctype';
import { getBaseUrl } from '../../main';
//import { MatAutocompleteModule } from '@angular/material/autocomplete';
//import { FormControl } from '@angular/forms';
// import { DadataConfig, DadataType, DadataSuggestion, DadataAddress } from '@kolkov/ngx-dadata';
import { MatAutocompleteTrigger } from '@angular/material/autocomplete';
import { ViewChild } from '@angular/core';
import { EnrolleesEducation } from '../models/enrolleeseducation';
import { EducationLevels } from '../models/educationlevels';
import { LKAUser } from '../models/lkauser';
import { ActivatedRoute } from '@angular/router';


@Component({
  selector: 'profile',
  templateUrl: './profile.component.html',
  providers: [DataService]
})
export class ProfileComponent implements OnInit {

  public chosenAddress: string;
  baseUrl: string;
  enrollee: Enrollee = new Enrollee();
  enrolleeId: number;
  relative: Relative = new Relative();
  enrolleesdoc: EnrolleesDoc = new EnrolleesDoc();
  enrolleeseducation: EnrolleesEducation = new EnrolleesEducation();
  isProfileDisabled: boolean = false;
  profile: Profile;
  lkauser: LKAUser;
  relativeTypes = ["Mother",
    "Father",
    "Brother",
    "Sister",
    "Daughter",
    "Son",
    "Wife",
    "Husband"];

  addresses: Array<string> = [];

  tableModeRelative: boolean = true;
  tableModeDoc: boolean = true;
  docTypeSelected: DocType;
  eduLevelSelected: EducationLevels;
  docSelected: EnrolleesDoc;

  public progress: number;
  public message: string;
  @Output() public onUploadFinished = new EventEmitter();
  //@ViewChild(MatAutocompleteTrigger, { read: MatAutocompleteTrigger }) inputAutoComplete: MatAutocompleteTrigger;
 // @ViewChild(MatAutocompleteTrigger, { read: MatAutocompleteTrigger, static: true }) inputAutoComplete: MatAutocompleteTrigger;
 //@ViewChild('inputAutoComplete', { static: true }) inputAutoComplete: MatAutocompleteTrigger;

  constructor(private dataService: DataService, private http: HttpClient, private route: ActivatedRoute) {

  }

  ngOnInit() {
    this.enrolleeId = Number(this.route.snapshot.paramMap.get('id'));
    this.loadData();    // загрузка данных при старте компонента
    this.isProfileDisabled = false;
  }
  // получаем данные через сервис
  loadData() {

    this.dataService.getFullProfile(this.enrolleeId)
      .subscribe((data: Profile) => this.profile = data);

    this.dataService.getCurrentUser() 
      .subscribe((data: LKAUser) => this.lkauser = data);
  }
  // сохранение данных
  save() {
    this.dataService.updateProfile(this.profile)
      .subscribe(() => this.loadData()); 
  }

  updatestatus(status: number) {
    this.profile.enrolleesstatus.profileStatus = status;
    this.dataService.updateEnrolleesStatus(this.profile.enrollee, this.profile.enrolleesstatus)
      .subscribe(() => true);
  }
  // -------------- do with relative ------------------
  getmaximalid() {
    var maxid = 1;
    this.profile.relatives.forEach((value, index) => {
      if (value.id > maxid) maxid = value.id;
    });
    return maxid;
  }

  saveRelative() {
    this.profile.relatives.forEach((value, index) => {
      if (value.id == this.relative.id) {
        value.name = this.relative.name;
        value.phone = this.relative.phone;
        value.type = this.relative.type;
        value.changed = true;
      };
    });
    this.relative = new Relative();
  }

  editRelative(r: Relative) {
    this.relative = r; 
  }
  cancelChangesRelative() {
    this.relative = new Relative();
    this.tableModeRelative = true;
  }
  deleteRelative(r: Relative) {
    /* for (var i = 0; i <= this.profile.relatives.length - 1; i++) {
      if (this.profile.relatives[i].id == r.id) {
        this.profile.relatives.reduce()
      }
    } */
    this.profile.relatives.forEach((value, index) => {
      if (value.id == r.id) this.profile.relatives.splice(index, 1);
    });
    // this.dataService.deleteEnrollee(p.id)
    //  .subscribe(data => this.loadEnrollees());
  }
  addRelative() {
    // this.cancelChanges();
    // this.tableModeRelative = false;
    this.relative = new Relative();
    this.relative.changed = true;
    this.relative.id = this.getmaximalid()+1;
    this.profile.relatives.push(this.relative);
  }

  // -------------- do with education ------------------
  getmaximalidedu() {
    var maxid = 1;
    this.profile.education.forEach((value, index) => {
      if (value.id > maxid) maxid = value.id;
    });
    return maxid;
  }

  saveEdu() {
    /*this.profile.docs.forEach((value, index) => {
      if (value.id == this.enrolleesdoc.id) {
        value.authority = this.enrolleesdoc.authority;
        value.issueDate = this.enrolleesdoc.issueDate;
        value.issuedBy = this.enrolleesdoc.issuedBy;
        value.number = this.enrolleesdoc.number;
        value.series = this.enrolleesdoc.series;
        value.type = this.enrolleesdoc.type;
        value.docTypeId = this.enrolleesdoc.docTypeId;
        value.changed = true;
      };
    });*/
    this.enrolleeseducation = new EnrolleesEducation();
  }

  editEdu(d: EnrolleesEducation) {
    if (d.educationLevelId) {
      this.eduLevelSelected = this.profile.educationlevels.find(x => x.id == d.educationLevelId);
    }
    this.enrolleeseducation = d;
  }
  cancelChangesEdu() {
    this.enrolleeseducation = new EnrolleesEducation();
  }
  deleteEdu(d: EnrolleesEducation) {
    this.profile.education.forEach((value, index) => {
      if (value.id == d.id) this.profile.education.splice(index, 1);
    });
  }
  addEdu() {
    this.enrolleeseducation = new EnrolleesEducation();
    this.enrolleeseducation.id = this.getmaximalidedu() + 1;
    this.enrolleeseducation.enrolleeID = this.profile.enrollee.id;
    this.profile.education.push(this.enrolleeseducation);
  }

  onEduLevelChange() {
    if (this.enrolleeseducation) {
      this.enrolleeseducation.educationLevelId = this.eduLevelSelected.id;
      this.enrolleeseducation.educationLevelName = this.eduLevelSelected.name;
    }
  }

  onDocChange() {
      if (this.enrolleeseducation) {
        this.enrolleeseducation.enrolleesDocId = this.docSelected.id;
        this.enrolleeseducation.enrolleesDocName = this.docSelected.type + " " + this.docSelected.series + this.docSelected.number;
      }
   }
  // -------------- do with docs ------------------
  getmaximaliddoc() {
    var maxid = 1;
    this.profile.docs.forEach((value, index) => {
      if (value.id > maxid) maxid = value.id;
    });
    return maxid;
  }

  saveDoc() {
    this.profile.docs.forEach((value, index) => {
      if (value.id == this.enrolleesdoc.id) {
        value.authority = this.enrolleesdoc.authority;
        value.issueDate = this.enrolleesdoc.issueDate;
        value.issuedBy = this.enrolleesdoc.issuedBy;
        value.number = this.enrolleesdoc.number;
        value.series = this.enrolleesdoc.series;
        value.type = this.enrolleesdoc.type;
        value.docTypeId = this.enrolleesdoc.docTypeId;
        value.changed = true;
      };
    });
    this.enrolleesdoc = new EnrolleesDoc();
  }

  editDoc(d: EnrolleesDoc) {
    if (d.docTypeId) {
      this.docTypeSelected = this.profile.doctypes.find(x => x.id == d.docTypeId);
    }
    this.enrolleesdoc = d;
  }
  cancelChangesDoc() {
    this.enrolleesdoc = new EnrolleesDoc();
    this.tableModeDoc = true;
  }
  deleteDoc(d: EnrolleesDoc) {
    this.profile.docs.forEach((value, index) => {
      if (value.id == d.id) this.profile.docs.splice(index, 1);
    });
  }
  addDoc() {
    this.enrolleesdoc = new EnrolleesDoc();
    this.enrolleesdoc.changed = true;
    this.enrolleesdoc.canBeDeleted = true;
    this.enrolleesdoc.id = this.getmaximaliddoc() + 1;
    this.profile.docs.push(this.enrolleesdoc);
  }

  onDocTypeChange() {
    if (this.enrolleesdoc) {
      this.enrolleesdoc.docTypeId = this.docTypeSelected.id;
      this.enrolleesdoc.type = this.docTypeSelected.name;
    }
  }

  // deal with files
  upload(files, d: EnrolleesDoc) {
    if (files.length == 0)
      return;

    var httpParams = new HttpParams()
      .append("enrolleeId", this.profile.enrollee.id.toString())
      .append("docId", d.id.toString());


    const formData = new FormData();

    for (const file of files) {
      formData.append(file.name, file);
    }

    this.http.post('/api/files', formData, { reportProgress: true, observe: 'events', params: httpParams })
      .subscribe(event => {
        if (event.type === HttpEventType.UploadProgress)
          this.progress = Math.round(100 * event.loaded / event.total);
        else if (event.type === HttpEventType.Response) {
          this.setFileId(event.body,d);
          this.message = 'Upload success ' + Number(event.body);
          this.onUploadFinished.emit(event.body);
        }
      });
  }

  setFileId(body, d: EnrolleesDoc) {
    let z = 1;
    for (let _d of this.profile.docs) {
      if (_d.id == d.id) {
        _d.fileId = Number(body); break;
      }
    }
  }

  downloadfile(d: EnrolleesDoc)
  {
    window.open(document.baseURI+'api/files/'+d.fileId, "_blank");
  }

  onAddressSelected()
  {
    this.http.post('https://suggestions.dadata.ru/suggestions/api/4_1/rs/suggest/address',
      JSON.stringify({ query: this.profile.enrollee.address }), {
        headers: {
          "Content-Type": "application/json",
          "Accept": "application/json",
          "Authorization": "Token 60237fd71bcfe50d953a8f9e5aca5a22bd00452c"
        } })
      .subscribe(result => {
        this.SelectAddress(result);
        }
      );
  }

  SelectAddress(suggestions) {
    this.addresses = [];
    for (let _a of suggestions.suggestions) {
      this.addresses.push(_a.unrestricted_value);
    }
    //this.inputAutoComplete.openPanel();
  }

  openPanel(trigger) {
    trigger.openPanel();
  }
  /* config: DadataConfig = {
    apiKey: '60237fd71bcfe50d953a8f9e5aca5a22bd00452c',
    type: DadataType.address
  };

  onAddressSelected(event: DadataSuggestion) {
    const addressData = event.data as DadataAddress;
    console.log(addressData);
  }*/

  //download() {
  //  this.http.get('/api/files/0', { observe: 'response', responseType: 'blob' }).subscribe(response => {
  //    let fileName = response.headers.get('content-disposition')?.split(';')[1].split('=')[1];
  //    //let blob:any = new Blob([response.body], { type: 'text/json; charset=utf-8' });
  //    let blob: Blob = response.body as Blob;
  //    let a = document.createElement('a');
  //    a.download = fileName;
  //    a.href = window.URL.createObjectURL(blob);
  //    a.click();
  //    //const url= window.URL.createObjectURL(blob);
  //    //window.open(url);
  //    //window.location.href = response.url;
  //    //fileSaver.saveAs(blob, 'employees.json');
  //  }), error => console.log('Error downloading the file'),
  //    () => console.info('File downloaded successfully');

  ////}

}
