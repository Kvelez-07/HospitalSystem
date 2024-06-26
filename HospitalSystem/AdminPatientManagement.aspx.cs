﻿using HospitalSystem.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.UI.WebControls;

namespace HospitalSystem
{
    public partial class AdminPatientManagement : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        protected void btnAddPatient_Click(object sender, EventArgs e)
        {
            string patientFilePath = Server.MapPath("~/DB/patient.txt");

            // Collect data from form fields
            string name = txtName.Text.Trim();
            string lastName1 = txtLastName1.Text.Trim();
            string lastName2 = txtLastName2.Text.Trim();
            string nic = txtNIC.Text.Trim();
            string civilStatus = txtCivilStatus.Text.Trim();
            string birthDate = txtBirthDate.Text.Trim();
            string phone = txtPhone.Text.Trim();
            string email = txtEmail.Text.Trim();
            string residency = txtResidency.Text.Trim();

            // Prepare patient data line
            string patientData = $"{name};{lastName1};{lastName2};{nic};{civilStatus};{birthDate};{phone};{email};{residency};";

            // Append patient data to file
            File.AppendAllText(patientFilePath, Environment.NewLine + patientData);

            // Clear form fields after adding patient
            ClearFormFields();

            ClientScript.RegisterStartupScript(this.GetType(), "alert", "showSuccessAlert('Patient added successfully!');", true);
        }

        protected void btnEditPatient_Click(object sender, EventArgs e)
        {
            string emailToEdit = txtEmail.Text.Trim(); // Edit based on email address

            string patientFilePath = Server.MapPath("~/DB/patient.txt");
            List<string> lines = new List<string>(File.ReadAllLines(patientFilePath));

            bool patientFound = false;

            for (int i = 0; i < lines.Count; i++)
            {
                string[] patientData = lines[i].Split(';');

                if (patientData.Length > 7 && patientData[7] == emailToEdit)
                {
                    // Update patient data with form values
                    patientData[0] = txtName.Text.Trim();
                    patientData[1] = txtLastName1.Text.Trim();
                    patientData[2] = txtLastName2.Text.Trim();
                    patientData[3] = txtNIC.Text.Trim();
                    patientData[4] = txtCivilStatus.Text.Trim();
                    patientData[5] = txtBirthDate.Text.Trim();
                    patientData[6] = txtPhone.Text.Trim();
                    patientData[7] = txtEmail.Text.Trim(); // Update email if necessary
                    patientData[8] = txtResidency.Text.Trim();

                    // Join patient data back into a single line
                    lines[i] = string.Join(";", patientData);

                    patientFound = true;
                    break; // Exit loop once patient is found and updated
                }
            }

            if (patientFound)
            {
                // Rewrite the entire file with updated patient data
                File.WriteAllLines(patientFilePath, lines);

                // Optionally, clear the form fields after editing
                ClearFormFields();

                // Show success message (this can be implemented in DoctorPatientManagement.aspx)
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "showSuccessAlert('Patient updated successfully!');", true);
            }
            else
            {
                // Show error message (this can be implemented in DoctorPatientManagement.aspx)
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "showErrorAlert('Patient not found or could not be updated.');", true);
            }
        }

        protected void btnDeletePatient_Click(object sender, EventArgs e)
        {
            string emailToDelete = txtEmail.Text.Trim();

            string patientFilePath = Server.MapPath("~/DB/patient.txt");
            List<string> lines = new List<string>(File.ReadAllLines(patientFilePath));

            bool patientFound = false;

            for (int i = 0; i < lines.Count; i++)
            {
                string[] patientData = lines[i].Split(';');

                if (patientData.Length > 7 && patientData[7] == emailToDelete)
                {
                    lines.RemoveAt(i); // Remove patient data at index

                    // Rewrite the entire file without the deleted patient
                    File.WriteAllLines(patientFilePath, lines);

                    patientFound = true;
                    break; // Exit loop once patient is found and deleted
                }
            }

            if (patientFound)
            {
                ClearFormFields();

                // Show success message (this can be implemented in DoctorPatientManagement.aspx)
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "showSuccessAlert('Patient deleted successfully!');", true);
            }
            else
            {
                // Show error message (this can be implemented in DoctorPatientManagement.aspx)
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "showErrorAlert('Patient not found or could not be deleted.');", true);
            }
        }

        private void ClearFormFields()
        {
            // Clear all textboxes
            txtName.Text = string.Empty;
            txtLastName1.Text = string.Empty;
            txtLastName2.Text = string.Empty;
            txtNIC.Text = string.Empty;
            txtCivilStatus.Text = string.Empty;
            txtBirthDate.Text = string.Empty;
            txtPhone.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtResidency.Text = string.Empty;
        }
    }
}
