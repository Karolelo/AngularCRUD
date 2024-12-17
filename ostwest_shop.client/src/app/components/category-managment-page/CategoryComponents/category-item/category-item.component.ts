import { Component,Input,Output,EventEmitter } from '@angular/core';
import {Category} from '../../../../Intefraces/category';

@Component({
  selector: 'app-category-item',
  standalone: false,

  templateUrl: './category-item.component.html',
  styleUrl: './category-item.component.css'
})
export class CategoryItemComponent {
  @Input() category!:Category;
  @Output() newDeleteCategory = new EventEmitter<number>();

  deleteCategory(value: number){
    this.newDeleteCategory.emit(value);
  }
}
