import { error } from '@angular/compiler/src/util';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, FormBuilder } from '@angular/forms';
import { Validators } from '@angular/forms';
import { SubscriberServiceService } from 'src/app/Services/SubscriberService/subscriber-service.service';

@Component({
  selector: 'app-index',
  templateUrl: './index.component.html',
  styleUrls: ['./index.component.css']
})
export class IndexComponent implements OnInit {

  subscriberForm: FormGroup;
  formError : string = '';
  formSuccess : string = '';
  loading : boolean = false;
  finished : boolean = false;
  constructor(private fb: FormBuilder, private subscriberService: SubscriberServiceService) { }

  ngOnInit(): void {
    this.subscriberForm = this.fb.group({
      email: new FormControl('', [
        Validators.required,
        Validators.email]),
      firstName: new FormControl('', [
        Validators.required,
        Validators.minLength(4),
      ]),
      lastName: [''],
    }
    );
  }
  sleep = (milliseconds) => {
    return new Promise(resolve => setTimeout(resolve, milliseconds))
  }
  submitForm(): void{
    this.formError = '';
    if (this.isValidForm()){
      this.loading = true;
      this.subscriberService.SubmitSubscriber(this.subscriberForm.value).subscribe(
        async c=>{
          this.formSuccess = c.message;
          this.finished = true;
          await this.sleep(2000);
          this.formSuccess = '';
        },
        e => {this.formError = e.error.errors.join("\n");console.log(error);},
        () => this.loading = false
      );
    }
  }

  isValidForm(): boolean {
    if (this.subscriberForm.get('email').invalid){
      this.formError = "Incorrect format for email";
      return false;
    }
    if (this.subscriberForm.get('firstName').invalid){
      this.formError = "First name must have at least 4 characters";
      return false;
    }
    return true;
  }
  title: string = "This is a test title";
  text: string = "Hello from an index component";
}
