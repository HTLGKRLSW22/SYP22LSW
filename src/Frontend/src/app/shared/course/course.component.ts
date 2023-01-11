import { Component, Input, OnInit } from '@angular/core';
import { OfferDto } from 'src/app/swagger';

@Component({
  selector: 'app-course',
  templateUrl: './course.component.html',
  styleUrls: ['./course.component.scss']
})
export class CourseComponent implements OnInit {
  @Input() offerDto: OfferDto | null = null;

  percentage: number | null = null;
  days: string[] = ['Mo', 'Di', 'Mi', 'Do', 'Fr', 'Sa', 'So'];

  ngOnInit(): void {
    this.percentage = (this.offerDto?.currentCount ?? -1) / (this.offerDto?.maxCount ?? -1);
  }

  onBtnEnterCourseClick(): void {
    console.log('onBtnEnterCourseClick');
  }

  onBtnLeaveCourseClick(): void {
    console.log('onBtnLeaveCourseClick');
  }

}
