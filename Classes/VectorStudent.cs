using VectorGraphQL;

namespace VectorClasses
{
    public class VectorStudent
    {
        public string Pidm { get; set; }

        public string CsuId { get; set; }

        public string TermCode { get; set; }

        /// <summary>
        /// To store corresponding Vector id
        /// </summary>
        public string TermCodeId { get; set; }

        public string Email { get; set; }

        public string EName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        /// <summary>
        /// Undergraduate or graduate
        /// </summary>
        public string Level { get; set; }

        /// <summary>
        /// New or transfer?
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Main campus or online
        /// </summary>
        public string Site { get; set; }

        /// <summary>
        /// To store corresponding Vector id
        /// </summary>
        public string SiteId { get; set; }

        /// <summary>
        /// Status code, AS = active student
        /// </summary>
        public string StatusCode { get; set; }

        public DateTime BirthDate { get; set; }

        private bool _is23OrOlder;
        public bool Is23OrOlder
        {
            get
            {
                // do age check
                try
                {
                    DateTime birthdate = this.BirthDate;
                    DateTime startDate = this.StartDate;

                    // is greater than or equal to 23, 28 days before the start of the term?
                    bool is23OrOlder = (startDate - new TimeSpan(28, 0, 0, 0) - birthdate) >= new TimeSpan(8400, 0, 0, 0);

                    return is23OrOlder;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            set
            {
                _is23OrOlder = value;
            }
        }

        /// <summary>
        /// To store corresponding Vector id
        /// </summary>
        public string AgeRangeId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EnrollmentDepositDate { get; set; }

        public bool Into { get; set; }

        public bool IntoPathways { get; set; }

        public bool Exists { get; set; }

        public bool MeetsCreditsRequirement { get; set; }

        /// <summary>
        /// CSPH (School of Public Health) students may be squirrelly in that they are neither N or T (but are an E), but we still want to include them as being eligible
        /// </summary>
        public bool IsCSPH { get; set; }

        /// <summary>
        /// Guid from Foundry
        /// </summary>
        public Guid UserGuid { get; set; }

        /// <summary>
        /// Guid from Foundry
        /// </summary>
        public Guid VectorId { get; set; }

        public DateTime ModifiedDate { get; set; }

        public VectorStudent()
        {
        }

        public VectorStudent(IGetPersonById_Person person)
        {
            this.Email = person.Email;
            this.CsuId = person.ExternalUniqueId;
            this.FirstName = person.First;
            this.LastName = person.Last;
            this.VectorId = new Guid(person.PersonId);
            this.EName = person.Username;
        }

        public VectorStudent(string pidm, object[] sqlResults)
        {
            // Initially set Exists = false. If requirements for AlcoholEdu and SAP eligibility are met,
            // then adjust Exists below.
            Pidm = pidm;
            Exists = false;
            MeetsCreditsRequirement = false;

            if (sqlResults.Length > 0)
            {
                // Check if all data required for AlcoholEdu and SAP eligibility is present.
                // The second IsNullOrEmpty checks to see if an actual CSU ID 


                if (!string.IsNullOrEmpty(sqlResults[0].ToString()))
                {
                    CsuId = sqlResults[0].ToString();
                    //Level = sqlResults[1].ToString();
                    Level = (sqlResults[1].ToString() == "UG" ? "undergrad" : "graduate");
                    Type = sqlResults[2].ToString();
                    Site = sqlResults[3].ToString();

                    if (Site == "M")
                        Site = "Main";
                    else if (Site == "MC")
                        Site = "Online";

                    StatusCode = sqlResults[4].ToString();
                    BirthDate = DateTime.Parse(sqlResults[5].ToString());
                    StartDate = DateTime.Parse(sqlResults[6].ToString());
                    Into = sqlResults[7].ToString() == "Y";
                    Email = sqlResults[8].ToString();
                    FirstName = sqlResults[9].ToString();
                    LastName = sqlResults[10].ToString();
                    IntoPathways = IsPathways(sqlResults[11].ToString(), sqlResults[12].ToString());
                    TermCode = sqlResults[13].ToString();
                    EnrollmentDepositDate = !string.IsNullOrEmpty(sqlResults[14].ToString())
                        ? DateTime.Parse(sqlResults[14].ToString())
                        : new DateTime();
                    IsCSPH = false; //sqlResults[15].ToString() == "Yes";
                    Exists = true;
                    EName = sqlResults[16].ToString();

                    // want to ensure e-mail isn't null
                    if (string.IsNullOrEmpty(Email))
                        Email = EName + "@colostate.edu"; 
                }
            }
        }


        private bool IsPathways(string programCode1, string programCode2)
        {
            if ((!string.IsNullOrEmpty(programCode1) &&
                string.Equals(programCode1.Substring(0, 2), "N2") &&
                (string.Equals(programCode1.Substring(programCode1.Length - 2, 2), "UG") ||
                string.Equals(programCode1.Substring(programCode1.Length - 3), "GR"))) ||
                (!string.IsNullOrEmpty(programCode2) &&
                string.Equals(programCode2.Substring(0, 2), "N2") &&
                (string.Equals(programCode2.Substring(programCode2.Length - 2, 2), "UG") ||
                string.Equals(programCode2.Substring(programCode2.Length - 3), "GR"))))
            {
                return true;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return (FirstName + LastName + Email + TermCode).GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var other = obj as VectorStudent;
            if (other == null) return false;

            return FirstName == other.FirstName && LastName == other.LastName && Email == other.Email && TermCode == other.TermCode;
        }
    }
}
