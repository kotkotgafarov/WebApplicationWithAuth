import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Enrollee } from '../models/enrollee';
import { Profile } from '../models/profile';
import { Application } from '../models/application';
import { EnrolleesStatus } from '../models/enrolleesstatus';


@Injectable()
export class DataService {

  private baseUrl: string;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }

  getEnrollees(pageIndex?: number, pageSize?: number, sort?: string, desc?: string, filterName?: string) {
    return this.http.get("api/enrollees?PageNumber=" + pageIndex + "&pagesize=" + pageSize + "&OrderBy=" + sort + "&SortDirection=" + desc + "&FilterName=" + filterName);
  }

  getFullProfile(id: number) {
    return this.http.get(this.baseUrl+'api/profile/' + id);
  }

  updateProfile(profile: Profile) {
    return this.http.put(this.baseUrl +'api/profile/' + profile.enrollee.id, profile);
  }

  getApplication(id: number) {
    return this.http.get(this.baseUrl +'api/application/' + id);
  }

  updateApplication(application: Application) {
    return this.http.put(this.baseUrl +'api/application/' + application.enrollee.id, application);
  }

  getCurrentUser() {
    return this.http.get(this.baseUrl +'api/users/0');
  }

  updateEnrolleesStatus(enrollee: Enrollee, enrolleestatus: EnrolleesStatus) {
    return this.http.put(this.baseUrl +'api/applicationstatus/' + enrollee.id, enrolleestatus);
  }
}
