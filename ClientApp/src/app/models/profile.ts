import { Enrollee } from '../models/enrollee';
import { Relative } from '../models/relative';
import { EnrolleesDoc } from '../models/enrolleesdoc';
import { DocType } from '../models/doctype';
import { EducationLevels } from './educationlevels';
import { EnrolleesEducation } from './enrolleeseducation';
import { EnrolleesStatus } from './enrolleesstatus';

export class Profile {
  constructor(
    public enrollee: Enrollee,
    public relatives: Relative[],
    public docs: EnrolleesDoc[],
    public doctypes: DocType[],
    public educationlevels: EducationLevels[],
    public education: EnrolleesEducation[],
    public enrolleesstatus: EnrolleesStatus
  ) { }
}
