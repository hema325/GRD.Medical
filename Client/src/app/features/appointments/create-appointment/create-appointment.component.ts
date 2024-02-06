import { Component, ElementRef, ViewChild } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { MatStepper } from '@angular/material/stepper';
import { ActivatedRoute, Router } from '@angular/router';
import { Stripe, StripeCardCvcElement, StripeCardExpiryElement, StripeCardNumberElement, loadStripe } from '@stripe/stripe-js';
import { ToastrService } from 'ngx-toastr';
import { PaymentIntent } from 'src/app/models/billingInfos/payment-intent';
import { TimeSlot } from 'src/app/models/time-slots/time-slot';
import { AppointmentsService } from 'src/app/services/appointments.service';
import { BillingInfosService } from 'src/app/services/billing-infos.service';
import { LoaderService } from 'src/app/services/loader.service';
import { TimeSlotsService } from 'src/app/services/time-slots.service';
import { environment } from 'src/environments/environment.development';

@Component({
  selector: 'app-create-appointment',
  templateUrl: './create-appointment.component.html',
  styleUrls: ['./create-appointment.component.css']
})
export class CreateAppointmentComponent {

  scheduleForm = this.fb.group({
    date: ['', Validators.required],
    doctorId: ['', Validators.required],
    timeSlotId: ['', Validators.required]
  });

  paymentIntent?: PaymentIntent;

  min = new Date();
  availableTimeSlots: TimeSlot[] = [];
  isTimeSlotsLoaded = false;

  constructor(private fb: FormBuilder,
    private activatedRoute: ActivatedRoute,
    private timeSlotsService: TimeSlotsService,
    private loaderService: LoaderService,
    private toastrService: ToastrService,
    private billingInfosService: BillingInfosService,
    private appointmentsService: AppointmentsService,
    private router: Router) {
  }

  ngOnInit() {
    const docId = this.activatedRoute.snapshot.queryParamMap.get('docId');
    this.scheduleForm.controls.doctorId.setValue(docId);
    this.initStripe();
  }

  getAvailableTimeSlots() {
    const form = this.scheduleForm.value;
    if (form.doctorId && form.date)
      this.timeSlotsService
        .getAvailableTimeSlots(form.doctorId, new Date(form.date).toDateString())
        .subscribe(res => {
          this.availableTimeSlots = res;
          this.isTimeSlotsLoaded = true;
        });
  }

  @ViewChild("stepper") stepper?: MatStepper;

  getPaymentIntent() {
    const docId = this.scheduleForm.value.doctorId;
    if (docId)
      this.billingInfosService.createPaymentIntentId(docId).subscribe(res => {
        this.paymentIntent = res;
        this.stepper?.next();
      });
  }

  @ViewChild('cardNumber') cardNumberEle?: ElementRef;
  @ViewChild('cardExpiry') cardExpiryEle?: ElementRef;
  @ViewChild('cardCvc') cardCvcEle?: ElementRef;

  stripe: Stripe | null = null;
  stripeCardNumber?: StripeCardNumberElement;
  stripeCardExpiry?: StripeCardExpiryElement;
  stripeCardCvc?: StripeCardCvcElement;

  cardNumberError?: string;
  cardExpiryError?: string;
  cardCvcError?: string;

  cardNumberCompleted: boolean = false;
  cardExpiryCompleted: boolean = false;
  cardCvcCompleted: boolean = false;

  isPaymentFormValid() {
    return this.cardNumberCompleted &&
      this.cardExpiryCompleted &&
      this.cardCvcCompleted;
  }

  initStripe() {
    loadStripe(environment.publishablekey)
      .then(stripe => {
        this.stripe = stripe;
        const stripeElements = stripe?.elements();
        if (stripeElements) {
          this.stripeCardNumber = stripeElements.create('cardNumber');
          this.stripeCardNumber?.mount(this.cardNumberEle?.nativeElement);
          this.stripeCardNumber?.on('change', e => {
            this.cardNumberError = e.error?.message;
            this.cardNumberCompleted = e.complete;
          });

          this.stripeCardExpiry = stripeElements.create('cardExpiry');
          this.stripeCardExpiry?.mount(this.cardExpiryEle?.nativeElement);
          this.stripeCardExpiry?.on('change', e => {
            this.cardExpiryError = e.error?.message;
            this.cardExpiryCompleted = e.complete;
          });

          this.stripeCardCvc = stripeElements.create('cardCvc');
          this.stripeCardCvc?.mount(this.cardCvcEle?.nativeElement);
          this.stripeCardCvc?.on('change', e => {
            this.cardCvcError = e.error?.message;
            this.cardCvcCompleted = e.complete;
          });
        }
      });
  }

  pay() {

    if (!this.paymentIntent)
      throw new Error('No payment intent id.');

    this.loaderService.show('stripePayment');
    this.stripe?.confirmCardPayment(this.paymentIntent.clientSecret, {
      payment_method: {
        card: this.stripeCardNumber!
      }
    })
      .then(result => {
        if (!result?.paymentIntent) {
          this.toastrService.error(result.error.message);
          this.loaderService.hide('stripePayment');
          return;
        }

        this.createAppointment();

        this.loaderService.hide('stripePayment');
      }).catch(error => this.loaderService.hide('stripePayment'));
  }

  createAppointment() {
    const form = this.scheduleForm.value;
    if (form) {
      const appointment = {
        date: this.toUTCDate(form.date!, Number(form.timeSlotId)).toISOString().split('T')[0],
        doctorId: form.doctorId,
        timeSlotId: form.timeSlotId,
        paymentIntentId: this.paymentIntent!.id
      };

      this.appointmentsService.create(appointment).subscribe(res => {
        this.router.navigate(['/appointments/success'], { state: { appointmentId: res } });
        this.toastrService.success('appointment created successfully.');
      });
    }
  }

  toUTCDate(date: string, timeSlotId: number) {
    const timeSlot = this.availableTimeSlots.filter(ts => ts.id == timeSlotId)[0];
    const dateObj = new Date(date);
    const day = dateObj.getDate();
    const month = dateObj.getMonth() + 1;
    const year = dateObj.getFullYear();
    return new Date(year + '/' + month + '/' + day + ' ' + this.timeToDateTime(timeSlot.start).toTimeString());
  }

  timeToDateTime(time: any) {
    return new Date('1970-01-01 ' + time + ' UTC');
  }

}
