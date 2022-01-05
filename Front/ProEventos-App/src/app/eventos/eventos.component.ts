import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.scss']
})
export class EventosComponent implements OnInit {

  
  public eventos: any = [];
  public eventosFiltrados: any = [];
  showImage: boolean = true;
  private _filterList: string = '';
  
  // Filtro de Pesquisa

  
  public get filterList(): string{
    return this._filterList;
  }

  public set filterList(value: string){
    this._filterList = value;
    this.eventosFiltrados = this.filterList ? this.filterEvents(this.filterList) : this.eventos;
  }

  public filterEvents(filterFor: string): any { 

    filterFor = filterFor.toLocaleLowerCase();
    return this.eventos.filter(
      ((evento: { tema: string; local: string; }) => evento.tema.toLocaleLowerCase().indexOf(filterFor) !== -1 ||
                                                     evento.local.toLocaleLowerCase().indexOf(filterFor) !== -1
    ))
  }



  constructor(private http: HttpClient) { }



  ngOnInit(): void {
    this.getEventos();
  }

  //Mostra imagem e oculta
  show(){
    this.showImage = !this.showImage;
  }

  public getEventos(): void{
    this.http.get('https://localhost:5001/api/Eventos').subscribe(
      response => {
        this.eventos = response
        this.eventosFiltrados = this.eventos
      }, 
      error => console.log(error)
    );

  }


      
  

}
