import { Component, OnInit, ViewChild, AfterViewInit} from '@angular/core';
import { DataService } from '../services/data.service';
import { Enrollee } from '../models/enrollee';
import { MatPaginator } from '@angular/material/paginator';
import { PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { MatSort, Sort } from '@angular/material/sort';

@Component({
  selector: 'enrollees',
  templateUrl: './enrollees.component.html',
  providers: [DataService]
})
export class EnrolleesComponent implements AfterViewInit  {

  displayedColumns = ['id', 'name', 'address', 'phone', 'profileStatus', 'applicationStatus'];
  totalRecords = 0;

  @ViewChild(MatPaginator, { static: false }) paginator?: MatPaginator;
  @ViewChild(MatSort, { static: false }) sort: MatSort;

  enrollee: Enrollee = new Enrollee();   
  enrollees: Enrollee[];
  filterName: string = "";
  dataSource: MatTableDataSource<Enrollee>;
  
  tableMode: boolean = true;          

  constructor(private dataService: DataService) { }

  ngOnInit() {
    //this.loadEnrollees();    // загрузка данных при старте компонента
  }

  loadEnrollees() {
    let pageIndex = 0;
    let pageSize = 5;
    let sort = "";
    let desc = "";
     
    if (this.paginator != null) {
      pageIndex = this.paginator.pageIndex;
      pageSize = this.paginator.pageSize;
    }

    if (this.sort != null) {
      sort = this.sort.active;
      desc = this.sort.direction;
    }
    this.dataService.getEnrollees(pageIndex, pageSize, sort, desc, this.filterName)
      .subscribe((data: PageResult<Enrollee>) => this.ProcessRequestResult(data)); 
  }

  ProcessRequestResult(data: PageResult<Enrollee>) {
    this.enrollees = data.items;
    this.totalRecords = data.count;
    this.dataSource = new MatTableDataSource<Enrollee>(data.items);
  }

  ngAfterViewInit() {
    this.loadEnrollees();
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  // сохранение данных
  /*save() {
    if (this.enrollee.id == null) {
      this.dataService.createEnrollee(this.enrollee)
        .subscribe((data: Enrollee) => this.enrollees.push(data));
    } else {
      this.dataService.updateEnrollee(this.enrollee)
        .subscribe(data => this.loadEnrollees());
    }
    this.cancel();
  }
  editEnrollee(p: Enrollee) {
    this.enrollee = p;
  }
  cancel() {
    this.enrollee = new Enrollee();
    this.tableMode = true;
  }
  delete(p: Enrollee) {
    this.dataService.deleteEnrollee(p.id)
      .subscribe(data => this.loadEnrollees());
  }
  add() {
    this.cancel();
    this.tableMode = false;
  }*/

  handlePageEvent(event: PageEvent) {
    this.loadEnrollees();
  }

  announceSortChange(sortState: Sort) {
    this.loadEnrollees();
  }
}


class PageResult<T>
{
  count: number;
  pageIndex: number;
  pageSize: number;
  items: T[];
}
