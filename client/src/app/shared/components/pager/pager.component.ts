import { Component, Input, OnInit, Output , EventEmitter } from '@angular/core';
import { PageChangedEvent } from 'ngx-bootstrap/pagination';
@Component({
  selector: 'app-pager',
  templateUrl: './pager.component.html',
  styleUrls: ['./pager.component.css']
})
export class PagerComponent implements OnInit {

  @Input()  totalCount;
  @Input() pageNumber;
  @Input() pageSize;

  @Output() pageChanged = new EventEmitter<any>();
  constructor() { }
  ngOnInit(): void {
  }
  onPagerChange(event: any) {    
    this.pageChanged.emit(event.page);
  }
}
