import { Component, OnInit } from '@angular/core';
import {OfferDate, OfferDto, OffersService, ReplyDTO} from "../../swagger";

@Component({
  selector: 'app-courses-list',
  templateUrl: './courses-list.component.html',
  styleUrls: ['./courses-list.component.scss']
})
export class CoursesListComponent implements OnInit {

  allCourses: OfferDto[] = [];

  constructor(private offersService: OffersService) {}

  ngOnInit(): void {
    console.log('admin::courses-list - Courses List works');
    this.offersService.offersGetOffersGet()
      .subscribe((x: OfferDto[])=>{
        this.allCourses = x;
        console.table(x);
      });
  }

  deleteCourse(offerId: number): void {
    console.log(`admin::courses-list::deleteCourse - Button Delete clicked - offerId:${offerId}`);
    this.offersService.offersDeleteOfferDelete(offerId)
      .subscribe((x: ReplyDTO)=>{
        console.table(x);
        this.reloadField();
      });
  }

  editCourse(offerId: number): void {
    console.log(`admin::courses-list::editCourse - Button Edit clicked - offerId:${offerId}`);
    this.reloadField();
  }

  dateConverter(offerdates: OfferDate[]): string{
    if(offerdates.length === 0){
      return '';
    }
    if(offerdates[0]!==offerdates[offerdates.length-1]){
      return `${offerdates[0].startDate} - ${offerdates[offerdates.length-1].startDate}`;
    }
    return `${offerdates[0].startDate}`;
  }

  reloadField() : void{
    this.offersService.offersGetOffersGet()
      .subscribe((x: OfferDto[])=>{
        this.allCourses = x;
        console.table(x);
      });
  }
}
