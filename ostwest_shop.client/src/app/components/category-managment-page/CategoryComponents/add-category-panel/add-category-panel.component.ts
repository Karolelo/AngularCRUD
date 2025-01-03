import { Component } from '@angular/core';
import {CategoryService} from '../../CategoryService/category.service';

@Component({
  selector: 'app-add-category-panel',
  standalone: false,

  templateUrl: './add-category-panel.component.html',
  styleUrl: './add-category-panel.component.css'
})
export class AddCategoryPanelComponent {

  categoryName = '';
  constructor(private categoryService: CategoryService) { }
  addCategory(): void {
    if (this.categoryName.trim() !== '') {
      const category = { name: this.categoryName };
      this.categoryService.addCategory(category).subscribe(
        (response) => {
          console.log('Kategoria dodana:', response);
        },
        (error) => {
          console.error('Błąd podczas dodawania kategorii:', error);
        }
      );
      this.categoryName = '';
    } else {
      console.warn('Nazwa kategorii nie może być pusta!');
    }
  }
}
