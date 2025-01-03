import { Component,Input } from '@angular/core';
import {Product} from "../../../../Intefraces/product";
import { MatCardModule,MatCardImage } from '@angular/material/card';
import {MatButtonModule} from '@angular/material/button'
@Component({
  selector: 'app-item-card',
  standalone: false,

  templateUrl: './item-card.component.html',
  styleUrl: './item-card.component.css'
})
export class ItemCardComponent {
    @Input() product!: Product;
}
