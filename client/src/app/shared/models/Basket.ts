import {v4 as uuidv4} from 'uuid';
export interface IBasket { 
    id: string ; // đây la cua Basket
    items: IBasketItem[];   
}
export interface IBasketItem {
    id: number // id cua Product bên trong Basket
    productname: string;
    price: number;
    quantity: number;
    pictureUrl: string;
    brand: string;
    type: string;
}
//mỗi lần tạo Instance ta muốn nó là duy nhất
export class Basket implements IBasket {
    id = uuidv4(); 
    items: IBasketItem[] =  [];

}