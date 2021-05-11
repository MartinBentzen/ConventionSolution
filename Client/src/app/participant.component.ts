import { Component } from '@angular/core';
import { Service, Convention, Character, Speaker } from './service';
import { FormGroup, FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-participant',
  templateUrl: './participant.component.html',
  styleUrls: ['./participant.component.css']
})
export class ParticipantComponent {
  conventionForm: FormGroup;
  conventions: Convention[] = [];
  constructor(private service: Service, private fb: FormBuilder) {}

  ngOnInit() {
    this.conventionForm = this.fb.group({});

    this.service.getConventions().subscribe(resp => {
      this.conventions = resp;
    });
  }
  reserveConvention(id) {
    let reserve = true;

    this.service.allocateConvention(id, reserve).subscribe(() => {});
  }
  participateConvention() {}
}
