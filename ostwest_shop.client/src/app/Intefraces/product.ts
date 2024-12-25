import {Category} from './category';

export interface Product {

  id: number;
  name: string;
  price: number;
  img: ImageBitmap;
  magazine: {
    productId: number;
    quanity:number;
  }
  categories: Category[];

}
