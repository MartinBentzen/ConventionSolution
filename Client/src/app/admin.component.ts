import { Component } from '@angular/core';
import {
  Service,
  Convention,
  Character,
  Speaker,
  NewConvention
} from './service';
import { FormGroup, FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})
export class AdminComponent {
  public newConvention: NewConvention = new NewConvention();

  public characters: Character[] = [];
  public speakers: Speaker[] = [];
  description: string = '';
  conventionForm: FormGroup;

  constructor(private service: Service, private fb: FormBuilder) {}

  ngOnInit() {
    this.conventionForm = this.fb.group({
      character: [null],
      speaker: [null],
      numberOfSeats: [0],
      name: ''
    });

    this.service.getConventionRelatedData().subscribe(resp => {
      debugger;
      this.characters = resp.characters;
      this.speakers = resp.speakers;
      this.conventionForm.get('character').patchValue(resp.characters[0]);
    });
  }

  createConvention() {
    this.service.createConvention(this.newConvention).subscribe(() => {});
  }
  public onOptionsSelected() {
    this.description = this.conventionForm.value.character.description;
  }
  submit() {
    this.newConvention.maxCap = this.conventionForm.value.numberOfSeats;
    this.newConvention.topic = this.conventionForm.value.character.name;
    this.newConvention.name = this.conventionForm.value.name;
    this.newConvention.description = this.conventionForm.value.character.description;
    this.newConvention.speakerId = this.conventionForm.value.speaker;

    this.service.createConvention(this.newConvention).subscribe(() => {});
  }
}
