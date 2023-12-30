import { Component, ElementRef, EventEmitter, Input, Output, Renderer2, SimpleChanges, ViewChild } from '@angular/core';
import { CarouselComponent, OwlOptions } from 'ngx-owl-carousel-o';

@Component({
  selector: 'app-light-box',
  templateUrl: './light-box.component.html',
  styleUrls: ['./light-box.component.css']
})
export class LightBoxComponent {

  @Input() active = false;
  @Output() activeChange = new EventEmitter<boolean>();
  @Input() imagesUrl: string[] = [];
  @Input() curIdx: number = 0;

  customOptions: OwlOptions = {
    loop: false,
    mouseDrag: true,
    touchDrag: true,
    pullDrag: false,
    dots: false,
    navSpeed: 700,
    navText: ['', ''],
    responsive: {
      0: {
        items: 1
      }
    },
    nav: false,
    autoHeight: true,
    startPosition: 0
  }

  close() {
    this.active = false;
    this.activeChange.emit(false);
  }

  activate(curIdx: number = 0) {
    this.active = true;
    this.carousel?.to(curIdx.toString());
  }

  constructor(private renderer: Renderer2) { }

  @ViewChild('outlier') outlier?: ElementRef;
  @ViewChild('carouselContainer') carouselContainer?: ElementRef;
  @ViewChild('carousel') carousel?: CarouselComponent;

  ngAfterViewInit() {
    this.renderer.listen(this.outlier?.nativeElement, 'click', e => {
      if (!this.carouselContainer?.nativeElement.contains(e.target))
        this.close();
    })
  }
}
