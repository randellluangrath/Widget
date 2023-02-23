import {Component, EventEmitter, Input, Output} from '@angular/core';

@Component({
  selector: 'app-list-item',
  templateUrl: './list-item.component.html',
  styleUrls: ['./list-item.component.css'],
})
export class ListItemComponent {
  showClose: boolean = false;

  @Input()
  name: string = '';

  @Input()
  isSearch: boolean = false;

  @Output()
  removeItemClickedEvent: EventEmitter<any> = new EventEmitter();

  public removeItemClicked(): void {
    this.removeItemClickedEvent.emit();
  }
}
