Feature: SpecFlowFeatureStatus
	'Coz I'm a lazy one I wrote this to test all statuses in automated manner

Scenario: Check status for owned, but moderated property
	Given Authorized as owner
	And Property added to it's owner
	When Status button is pressed
	Then Status for owned, but moderated property can be found

Scenario: Check status for leased, but yet hasn't been approved property
	Given Authorized as leaser
	And Property added to it's leaser
	When Status button is pressed
	Then Status for leased, but yet hasn't been approved property can be found

Scenario: Check status for cohabitation, that yet hasn't been approved
	Given Authorized as cohabitant
	And Property added to it's cohabitant
	When Status button is pressed
	Then Status for cohabitation that yet hasn't been approved can be found

Scenario: Check status for accepted lease of moderated property
	Given Authorized as leaser
	And Property added to it's leaser
	And Lease/cohabitance is accepted
	When Status button is pressed
	Then Status for accepted lease of moderated property can be found

Scenario: Check status for accepted cohabitance of moderated property
	Given Authorized as cohabitant
	And Property added to it's cohabitant
	And Lease/cohabitance is accepted
	When Status button is pressed
	Then Status for accepted cohabitance of moderated property can be found

Scenario: Check status for owned and accepted property
	Given Authorized as owner
	And Property added to it's owner
	And Owned property is accepted
	When Status button is pressed
	Then Status for owned and accepted property can be found

Scenario: Check status for leased and accepted property
	Given Authorized as leaser
	And Property added to it's leaser
	And Lease/cohabitance is accepted
	When Status button is pressed
	Then Status for accepted lease can be found

Scenario: Check status for accepted cohabitance
	Given Authorized as cohabitant
	And Property added to it's cohabitant
	And Lease/cohabitance is accepted
	When Status button is pressed
	Then Status for accepted cohabitance can be found

	Scenario: Check status for owned and declined property
	Given Authorized as owner
	And Property added to it's owner
	And Owned property is declined
	When Status button is pressed
	Then Status for owned and declined property can be found

Scenario: Check status for accepted lease of declined property
	Given Authorized as leaser
	And Property added to it's leaser
	And Lease/cohabitance is accepted
	When Status button is pressed
	Then Status for for accepted lease of declined property can be found

Scenario: Check status for accepted cohabitance of declined property
	Given Authorized as cohabitant
	And Property added to it's cohabitant
	And Lease/cohabitance is accepted
	When Status button is pressed
	Then Status for accepted cohabitance of declined property can be found

Scenario: Check status for declined lease
	Given Authorized as owner
	And Property added to it's owner
	And Owned property is accepted 
	And Authorized as leaser
	And Property added to it's leaser
	And Lease/cohabitance is declined
	When Status button is pressed
	Then Status for declined lease can be found

Scenario: Check status for declined cohabitance
	Given Authorized as cohabitant
	And Property added to it's cohabitant
	And Lease/cohabitance is declined
	When Status button is pressed
	Then Status for declined cohabitance can be found

Scenario: Check profile for unconfirmed owner
	Given Authorized as owner
	When Status button is pressed
	Then Status for unconfirmed profile can be found

Scenario: Check profile for unconfirmed leaser
	Given Authorized as leaser
	When Status button is pressed
	Then Status for unconfirmed profile can be found

Scenario: Check profile for unconfirmed cohabitant
	Given Authorized as cohabitant
	When Status button is pressed
	Then Status for unconfirmed profile can be found

#Scenario: Check profile for moderated owner
#	Given Authorized as owner
#	And Confirmation form is filled
#	When Status button is pressed
#	Then Status for moderated profile can be found

#Scenario: Check profile for moderated leaser
#	Given Authorized as leaser
#	And Confirmation form is filled
#	When Status button is pressed
#	Then Status for moderated profile can be found

#Scenario: Check profile for moderated cohabitant
#	Given Authorized as cohabitant
#	And Confirmation form is filled
#	When Status button is pressed
#	Then Status for moderated profile can be found

#Scenario: Check profile for confirmed owner
#	Given Authorized as owner
#	And Profile is accepted
#	When Status button is pressed
#	Then Status for accepted profile can be found
#
#Scenario: Check profile for confirmed leaser
#	Given Authorized as leaser
#	And Profile is accepted
#	When Status button is pressed
#	Then Status for accepted profile can be found
#
#Scenario: Check profile for confirmed cohabitant
#	Given Authorized as cohabitant
#	And Profile is accepted
#	When Status button is pressed
#	Then Status for accepted profile can be found

#Scenario: Check profile for declined owner
#	Given Authorized as owner
#	And Confirmation form is filled
#	And Profile is declined
#	When Status button is pressed
#	Then Status for declined profile can be found

#Scenario: Check profile for declined leaser
#	Given Authorized as leaser
#	And Confirmation form is filled
#	And Profile is declined
#	When Status button is pressed
#	Then Status for declined profile can be found

#Scenario: Check profile for declined cohabitant
#	Given Authorized as cohabitant
#	And Confirmation form is filled
#	And Profile is declined
#	When Status button is pressed
#	Then Status for declined profile can be found

Scenario: Check status for non-existant property
	Given Authorized as owner
	And Property added to it's owner
	When Status button is pressed
	Then Status for non-existant property can be found