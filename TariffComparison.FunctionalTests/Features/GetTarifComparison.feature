Feature: GetTarifComparison

Background: Tarifs exist
	Given the following products exist:
		| Name                     | ProductType | UnconditionalCosts | PackageCosts | InclidedInPackage | ConsumptionCosts |
		| Basic electricity tariff | Basic       | 5                  | 0            | 0                 | 0.22             |
		| Packaged tariff          | Packaged    | 0                  | 800          | 4000              | 0.3              |

Scenario: Get tarif comparison when consumption is slightly below the package limit
	Given I am authenticated
	When I request tarif comparison with consumption '3500'
	Then the following product comparison is received:
		| TariffName               | AnnualCosts |
		| Packaged tariff          | 800         |
		| Basic electricity tariff | 830         |

Scenario: Get tarif comparison when consumption is slightly above the package limit
	Given I am authenticated
	When I request tarif comparison with consumption '4500'
	Then the following product comparison is received:
		| TariffName               | AnnualCosts |
		| Packaged tariff          | 950         |
		| Basic electricity tariff | 1050        |

Scenario: Get tarif comparison when consumption is above the package limit
	Given I am authenticated
	When I request tarif comparison with consumption '6000'
	Then the following product comparison is received:
		| TariffName               | AnnualCosts |
		| Basic electricity tariff | 1380        |
		| Packaged tariff          | 1400        |

Scenario Outline: Get tarif comparison when consumption value is out of allowed range
	Given I am authenticated
	When I request tarif comparison with consumption '<Consumption>'
	Then my request fails with the status code 'BadRequest'

	Examples:
	| Consumption |
	| -1          |
	| 1000000001  |
