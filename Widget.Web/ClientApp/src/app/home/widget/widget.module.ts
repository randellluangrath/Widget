import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { WidgetComponent } from './widget.component';
import { CheckboxModule } from 'primeng/checkbox';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { CardModule } from 'primeng/card';
import { FlexLayoutModule } from '@angular/flex-layout';
import { ListItemComponent } from './list-item/list-item.component';
import { CardComponent } from './card/card.component';
import { LoadingIndicatorComponent } from './loading-indicator/loading-indicator.component';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    WidgetComponent,
    ListItemComponent,
    CardComponent,
    LoadingIndicatorComponent,
  ],
  imports: [
    CommonModule,
    CheckboxModule,
    ButtonModule,
    InputTextModule,
    CardModule,
    FlexLayoutModule,
    ProgressSpinnerModule,
    ReactiveFormsModule,
  ],
  exports: [WidgetComponent],
})
export class WidgetModule {}
