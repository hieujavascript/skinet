import { Component, OnInit } from '@angular/core';
import { from, Observable, of } from 'rxjs';
import { BreadcrumbService } from 'xng-breadcrumb';

@Component({
  selector: 'app-section-header',
  templateUrl: './section-header.component.html',
  styleUrls: ['./section-header.component.css']
})
export class SectionHeaderComponent implements OnInit {
  breadcrumb$: Observable<any[]>
  constructor(private breadcrumb: BreadcrumbService) { }

  ngOnInit(): void {
    this.getValue();
  }
  getValue() {
    this.breadcrumb$ = this.breadcrumb.breadcrumbs$;
  }
}
