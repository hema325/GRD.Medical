import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Advice } from 'src/app/models/advices/advice';
import { AdvicesService } from 'src/app/services/advices.service';

@Component({
  selector: 'app-advices-details',
  templateUrl: './advices-details.component.html',
  styleUrls: ['./advices-details.component.css']
})
export class AdvicesDetailsComponent {
  advice: Advice | null = null;

  constructor(private router: Router,
    private activatedRoute: ActivatedRoute,
    private adviceService: AdvicesService) {
    this.advice = this.router.getCurrentNavigation()?.extras.state as Advice;
  }

  ngOnInit() {
    let id = Number(this.activatedRoute.snapshot.paramMap.get('id'));
    if (!this.advice)
      this.adviceService.getById(id).subscribe(res => this.advice = res);

  }

}
