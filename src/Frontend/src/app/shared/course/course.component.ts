import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-course',
  templateUrl: './course.component.html',
  styleUrls: ['./course.component.scss']
})
export class CourseComponent implements OnInit {
  @Input() title: string | null = null;
  @Input() description: string | null = null;
  @Input() teachers: string[] | null = null;
  @Input() price: number | null = null;
  @Input() startDates: string[] | null = null;
  @Input() endDates: string[] | null = null;
  @Input() currentCount: number | null = null;
  @Input() maxCount: number | null = null;
  @Input() minCount: number | null = null;
  @Input() location: string | null = null;
  @Input() meetingPoint : string | null = null;
  @Input() enrolled: boolean | null = null;

  percentage: number | null = null;
  days: string[] = ['Mo', 'Di', 'Mi', 'Do', 'Fr', 'Sa', 'So'];

  ngOnInit(): void {
    this.percentage = (this.currentCount??-1) / (this.maxCount??-1);
  }

  onBtnEnterCourseClick(): void {
    console.log('onBtnEnterCourseClick');
  }

  onBtnLeaveCourseClick(): void {
    console.log('onBtnLeaveCourseClick');
  }

}
